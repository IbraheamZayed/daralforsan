using Core.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
            options =>
            {
                options.LoginPath = "/Common/Account/Login";
                options.LogoutPath = "/Common/Account/Logout";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration.GetValue<string>("AppSettings:SessionTimeout")));
                options.Cookie.Name = "DarAlforsan";
                options.Cookie.HttpOnly = false;
                options.ReturnUrlParameter = "returnUrl";
            });

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddMemoryCache();
ServiceInjection.Inject(builder.Services);

Core.Calendar.Date Date = new Core.Calendar.Date();
builder.Services.AddSingleton<Core.Calendar.Date>(Date);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(Convert.ToInt32(builder.Configuration.GetValue<string>("AppSettings:SessionTimeout")));
    options.Cookie.HttpOnly = true;
    // Make the session cookie essential
    options.Cookie.IsEssential = true;
});

//max form size
builder.Services.Configure<FormOptions>(form =>
{
    form.MultipartBodyLengthLimit = Int32.MaxValue;
    form.ValueCountLimit = Int32.MaxValue;
    form.ValueLengthLimit = Int32.MaxValue;
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//Custom Middleware to handle request culture 

app.Use((Func<Microsoft.AspNetCore.Http.HttpContext, Func<System.Threading.Tasks.Task>, System.Threading.Tasks.Task>)(async (context, next) =>
{
    string Lang = Core.UI.UILanguage.GlobalIsoLocalName;
    if (context.User.Identity.IsAuthenticated)
    {
        Lang = context.GetCurrentLanguage();
        Core.Security.Identity Identity = context.RequestServices.GetService<Core.Security.Identity>();
        Identity.UserID = context.GetCurrentUserID();
        Identity.BranchID = context.GetCurrentBranchID();
        Identity.Theme = context.GetCurrentTheme();
        Identity.IsoLanguageName = context.GetCurrentLanguageIsoName();
    }
    CultureInfo.CurrentCulture = new CultureInfo("en");
    CultureInfo.CurrentUICulture = new CultureInfo(Lang);
    await next.Invoke();
}));

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();