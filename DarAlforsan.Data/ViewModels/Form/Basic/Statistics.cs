using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class Statistics
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleAr", ResourceType = typeof(DBResources.Statistics))]
        public string TitleAr { get; set; } = string.Empty;
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleEn", ResourceType = typeof(DBResources.Statistics))]
        public string TitleEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string Img { get; set; } = string.Empty;
        [Display(Name = "Count", ResourceType = typeof(DBResources.Statistics))]
        public int Count { get; set; }
    }
}