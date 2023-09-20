using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class SaidAboutUs
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(DBResources.SaidAboutUs))]
        public string Name { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string MessageAr { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string MessageEn { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        public string Img { get; set; } = string.Empty;
    }
}