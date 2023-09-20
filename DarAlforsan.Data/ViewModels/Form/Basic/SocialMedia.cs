using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class SocialMedia
    {
        public long ID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(DBResources.SocialMedia))]
        public string Name { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        [Display(Name = "Icon", ResourceType = typeof(DBResources.SocialMedia))]
        public string Img { get; set; } = string.Empty;
        [StringLength(100)]
        [Required]
        [Display(Name = "Url", ResourceType = typeof(DBResources.SocialMedia))]
        public string Url { get; set; } = string.Empty;
    }
}
