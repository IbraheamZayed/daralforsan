using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class News
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleAr", ResourceType = typeof(DBResources.News))]
        public string TitleAr { get; set; } = string.Empty;
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleEn", ResourceType = typeof(DBResources.News))]
        public string TitleEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string DetailsAr { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string DetailsEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string MainImg { get; set; } = string.Empty;
        [Required]
        public string Imgs { get; set; } = string.Empty;
        [Display(Name = "ViewsCount", ResourceType = typeof(DBResources.News))]
        public int ViewsCount { get; set; }
        [Display(Name = "AddDate", ResourceType = typeof(DBResources.News))]
        public DateTime? AddDate { get; set; }
    }
}