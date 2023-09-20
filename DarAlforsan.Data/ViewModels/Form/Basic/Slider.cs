using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class Slider
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleAr", ResourceType = typeof(DBResources.Slider))]
        public string TitleAr { get; set; } = string.Empty;
        [StringLength(100)]
        [Required]
        [Display(Name = "TitleEn", ResourceType = typeof(DBResources.Slider))]
        public string TitleEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string DetailsAr { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string DetailsEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string Img { get; set; } = string.Empty;
    }
}