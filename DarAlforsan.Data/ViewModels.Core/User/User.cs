using System.ComponentModel.DataAnnotations;
//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace ViewModels
{
    //\///////////////////////////////////////////////////////////////////////////////////////////
    public class UserViewModel
    {
        public Models.User User { set; get; }
        public long UserID { get; set; }
        public bool IsEnabled { get; set; }
        public string LocalName { get; set; }
        public string LatinName { get; set; }
        public string UserName { get; set; }
        public string MobileNo { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Password", ResourceType = typeof(DBResources.Security))]
        public string Password { get; set; }
        public string VClassUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
       
        [Display(Name = "LastLoginDays")]
        public int LastLoginDays { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
    }

    public class UserInfo
    {
        public string Img { get; set; }
        public long UserID { get; set; }
        public string LocalName { get; set; }
        public string LatinName { get; set; }
        public string IdentityNo { get; set; }
        public string MobileNo { get; set; }
        public string NationName { get; set; }
        public string SignatureFileName { get; set; }
        public string FunctionName { get; set; }
        public bool IsCommit { get; set; }
    }
    //\///////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////
