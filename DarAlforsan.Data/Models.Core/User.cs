using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User
    {
        public long UserID { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "LocalName")]
        public string LocalName { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "LatinName")]
        public string? LatinName { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "UserName")]
        public string UserName { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "Password")]
        public string Password { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "Email")]
        public string? Email { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "MobileNo")]
        public string? MobileNo { get; set; }
        public string? PhotoName { get; set; }
        public string Id { get; set; }
        public string? DeviceID { get; set; }

        [Display(ResourceType = typeof(DBResources.Security), Name = "AddDate")]
        public DateTime AddDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? Theme { get; set; }
        public int UserClassRepeated { get; set; }
        public int MaxPeriodCountInDay { get; set; }

        public long? UILanguageID { get; set; }
        [ForeignKey(nameof(UILanguageID))]
        public virtual UILanguage UILanguage { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "IsActive")]
        public bool IsActive { get; set; }
        public string? Token { get; set; }
        public string? VClassUrl { get; set; }
        [Display(ResourceType = typeof(DBResources.Security), Name = "IsEnabled")]
        public bool IsEnabled { get; set; }
        public bool Ishidden { get; set; }
        public string? SignatureFileName { get; set; }
        public long BranchID { get; set; }
        [ForeignKey(nameof(BranchID))]
        public virtual Branch Branch { get; set; }
    }
}