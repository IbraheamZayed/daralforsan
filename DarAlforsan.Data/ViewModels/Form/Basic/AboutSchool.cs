using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class AboutSchool
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "AddressAr", ResourceType = typeof(DBResources.AboutSchool))]
        public string AddressAr { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "AddressEn", ResourceType = typeof(DBResources.AboutSchool))]
        public string AddressEn { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "MobileNo", ResourceType = typeof(DBResources.AboutSchool))]
        public string MobileNo { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "WhatsAppNo", ResourceType = typeof(DBResources.AboutSchool))]
        public string WhatsAppNo { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Email", ResourceType = typeof(DBResources.AboutSchool))]
        public string Email { get; set; }
        [StringLength(500)]
        [Required]
        public string DetailsAr { get; set; }
        [StringLength(500)]
        [Required]
        public string DetailsEn { get; set; }
        [Display(Name = "Lat", ResourceType = typeof(DBResources.AboutSchool))]
        public float Lat { get; set; }
        [Display(Name = "Lng", ResourceType = typeof(DBResources.AboutSchool))]
        public float Lng { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Logo", ResourceType = typeof(DBResources.AboutSchool))]
        public string Logo { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "RegisterationUrl", ResourceType = typeof(DBResources.AboutSchool))]
        public string? RegisterationUrl { get; set; }
    }
}