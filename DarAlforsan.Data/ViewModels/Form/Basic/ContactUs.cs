using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class ContactUs
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(DBResources.ContactUs))]
        public string Name { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Email", ResourceType = typeof(DBResources.ContactUs))]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "MobileNo", ResourceType = typeof(DBResources.ContactUs))]
        public string MobileNo { get; set; }
        [StringLength(250)]
        [Required]
        [Display(Name = "Message", ResourceType = typeof(DBResources.ContactUs))]
        public string Message { get; set; }
    }
}