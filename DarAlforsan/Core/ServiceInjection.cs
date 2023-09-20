namespace Core.Data
{
    public class ServiceInjection
    {
        public static void Inject(IServiceCollection _Services)
        {
            //1 : Global
            Core.Calendar.Date Date = new Core.Calendar.Date();
            _Services.AddSingleton<Core.Calendar.Date>(Date);
            //Inject Identity
            _Services.AddScoped<Core.Security.Identity>();

            _Services.AddScoped<Services.SocialMedia>();
        }
    }
}