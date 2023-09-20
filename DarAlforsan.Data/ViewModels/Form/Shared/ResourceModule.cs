using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class ResourceModule
    {
        public long ModuleID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(DBResources.Resources))]
        public string Name { get; set; }
        [StringLength(255)]
        [Required]
        [Display(Name = "LocalName", ResourceType = typeof(DBResources.Resources))]
        public string LocalName { get; set; }
        [StringLength(255)]
        [Required]
        [Display(Name = "LatinName", ResourceType = typeof(DBResources.Resources))]
        public string LatinName { get; set; }
    }
}