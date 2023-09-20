using Core.Caching;

namespace Models
{
    [Cacheable("ResourceModule")]
    public class ResourceModule
    {
        public long ModuleID { get; set; }
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string LatinName { get; set; }

        public virtual ICollection<Resource> Resources { get; set; } = new HashSet<Resource>();
    }
}