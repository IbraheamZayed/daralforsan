using System.ComponentModel.DataAnnotations;
//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace ViewModels.Security
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class User
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LocalName { get; set; }
        public string LatinName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Theme { get; set; }
        public Nullable<DateTime> LastLoginDate { get; set; }
        public Nullable<long> UILanguageID { get; set; }
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class Role
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string LatinName { get; set; }
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class Credential
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
        public bool EnableRegisterLink { get; set; }
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class ChangePassword
    {
        public long UserID { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////