using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class SchoolSystem
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleAr", ResourceType = typeof(DBResources.SchoolSystem))]
        public string TitleAr { get; set; } = string.Empty;
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleEn", ResourceType = typeof(DBResources.SchoolSystem))]
        public string TitleEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string MessageAr { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string MessageEn { get; set; } = string.Empty;
    }
}