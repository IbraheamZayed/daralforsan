using Core.Caching;

namespace Models
{
    [Cacheable("UILanguage")]
    public class UILanguage
    {
        public long LanguageID { get; set; }
        public string Name { get; set; }
        public string? LocalName { get; set; }
        public string? LatinName { get; set; }
        public string Direction { get; set; }
        public string? ISOLanguageName { get; set; }
        public bool? IsDefault { set; get; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}