namespace Models
{
    public class SchoolFruits
    {
        public long ID { get; set; }
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;

        public virtual ICollection<SchoolFruitsDetails> FruitsDetails { get; set; } = new HashSet<SchoolFruitsDetails>();
    }
}