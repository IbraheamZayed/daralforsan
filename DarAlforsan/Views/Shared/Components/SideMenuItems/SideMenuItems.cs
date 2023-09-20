using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
namespace Dars360.V3.Views.Shared.Components.SideMenuItems
{
    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    [ViewComponent(Name = "SideMenuItems")]
    public class SideMenuItems : ViewComponent
    {
        private long BranchID { get; set; }
        private   long UserID { get; set; }
        

        //-----------------------------------------------------------------------------------------
        public SideMenuItems(IHttpContextAccessor ContextAccessor)
        {
         
           
            this.UserID = ContextAccessor.HttpContext.GetCurrentUserID();
        }
        //-----------------------------------------------------------------------------------------
        public IViewComponentResult Invoke(ViewModels.List.SideMenu model)
        {
            return View("SideMenuItems", model);
        }
    }
    //\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
}
//\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
