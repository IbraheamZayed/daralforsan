using Core.Caching;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Cacheable("Resource")]
    public class Resource
    {
        public long ResourceID { get; set; }
        public string Name { get; set; }
        public string LocalValue { get; set; }
        public string LatinValue { get; set; }
        public long ModuleID { set; get; }

        [ForeignKey(nameof(ModuleID))]
        public virtual ResourceModule ResourceModule { get; set; }
    }
}