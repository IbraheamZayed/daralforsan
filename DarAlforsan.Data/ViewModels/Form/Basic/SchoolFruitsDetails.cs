using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class SchoolFruitsDetails
    {
        public long ID { get; set; }
        [Display(Name = "PageTitle", ResourceType = typeof(DBResources.SchoolFruits))]
        public long FruitsID { get; set; }
        [StringLength(250)]
        [Required]
        [Display(Name = "TitleAr", ResourceType = typeof(DBResources.SchoolFruits))]
        public string TitleAr { get; set; } = string.Empty;
        [StringLength(250)]
        [Required]
        [Display(Name = "TitleEn", ResourceType = typeof(DBResources.SchoolFruits))]
        public string TitleEn { get; set; } = string.Empty;
        public IEnumerable<ViewModels.Form.SchoolFruits>? SchoolFruits { get; set; }
    }
}