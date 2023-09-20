using System.ComponentModel.DataAnnotations;

namespace ViewModels.Form
{
    public class Resource
    {
        public long ResourceID { get; set; }
        [Display(Name = "Module", ResourceType = typeof(DBResources.Resources))]
        public long ModuleID { get; set; }
        [StringLength(100)]
        [Required]
        [Display(Name = "Name", ResourceType = typeof(DBResources.Resources))]
        public string Name { get; set; }
        [StringLength(255)]
        [Required]
        [Display(Name = "LocalName", ResourceType = typeof(DBResources.Resources))]
        public string LocalValue { get; set; }
        [StringLength(255)]
        [Required]
        [Display(Name = "LatinName", ResourceType = typeof(DBResources.Resources))]
        public string LatinValue { get; set; }

        public IEnumerable<ViewModels.Form.ResourceModule> ResourceModules { get; set; }
    }
}