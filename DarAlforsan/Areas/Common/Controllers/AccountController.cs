using Core;
using Core.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DarAlforsan.Areas.Common.Controllers
{
    [Area("Common")]
    public class AccountController : BaseController
    {
        public IConfiguration config;
        //-----------------------------------------------------------------------------------------
        public AccountController(IConfiguration _Con) : base(_Con)
        {
            this.config = _Con;
        }
        //-----------------------------------------------------------------------------------------
        public IActionResult Index()
        {
            return View();
        }
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewModels.Security.Credential credential = new ViewModels.Security.Credential();
            credential.ReturnUrl = returnUrl;
            return View(credential);
        }
        // -----------------------------------------------------------------------------------------
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(ViewModels.Security.Credential Account)
        {
            try
            {
                Security.UserManager umanager = new Security.UserManager();
                bool IsAuthenticated = umanager.SignIn(HttpContext, Account.UserName, Account.Password).Result;

                if (IsAuthenticated)
                {
                    if (!string.IsNullOrEmpty(Account.ReturnUrl))
                    {
                        if (Account.ReturnUrl.Length < 7)
                            return RedirectToAction("Index", "Home", new { area = "" });
                        else
                            return Redirect(Account.ReturnUrl);
                    }
                    else
                        return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    ViewBag.Message = DBResources.Users.ErrorLoginFailed;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
            }
            return View(Account);
        }
        //-----------------------------------------------------------------------------------------
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        //-----------------------------------------------------------------------------------------
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword()
        {
            ViewModels.Security.ChangePassword vmodel = new ViewModels.Security.ChangePassword();
            return View(vmodel);
        }
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ViewModels.Security.ChangePassword VModel)
        {
            Services.User user = new Services.User(this.BranchID, UserID);
            VModel.UserID = UserID;
            EXResult res = user.ChangePassword(VModel);
            if (res.Status == EXResultStatus.Success)
            {
                ViewBag.MessageBox = Core.UI.MessageBox.CreateMsg(DBResources.Users.PasswordChanged, Core.UI.MessageBoxStatus.Success, Core.UI.MessageBoxBehavior.BootstapModal);
            }
            else
            {
                ViewBag.MessageBox = Core.UI.MessageBox.CreateMsg(DBResources.Users.ErrorCurrentPassword, Core.UI.MessageBoxStatus.Fail, Core.UI.MessageBoxBehavior.BootstapModal);
            }
            return View(VModel);
        }
        //-----------------------------------------------------------------------------------------
        public IActionResult ChangeTheme(string Theme)
        {
            Services.User user = new Services.User(this.BranchID, UserID);
            //current logged in user UserID
            EXResult res = user.ChangeTheme(UserID, Theme);
            string url = Request.Headers["Referer"].ToString();

            if (res.Status == EXResultStatus.Success)
            {
                HttpContext.SetCurrentTheme(Theme);
            }
            if (Theme.Contains("darsnew"))
            {
                url = "/Home/Index2";
            }
            else
            {
                if ((url.Contains("Index2")))
                    url = "/Home/Index";
            }
            //get current url 
            //redirect to the same view
            return Redirect(url);
        }
        //-----------------------------------------------------------------------------------------
        [AllowAnonymous]
        public IActionResult ChangeUILanguage(long UILanguageID)
        {
            Services.User user = new Services.User(this.BranchID, UserID);

            EXResult res = user.ChangeUILanguage(UserID, UILanguageID);
            if (res.Status == EXResultStatus.Success)
            {
                Services.UILanguage Service = new Services.UILanguage();
                var UILag = Service.GetByID(UILanguageID);
                HttpContext.SetCurrentLanguage(UILag.ISOLanguageName);
            }

            //get current url 
            string url = Request.Headers["Referer"].ToString();
            //redirect to the same view
            return Redirect(url);
        }
        //-----------------------------------------------------------------------------------------
        public IActionResult AccessDenied()
        {
            return View();
        }
        //-----------------------------------------------------------------------------------------
    }
}