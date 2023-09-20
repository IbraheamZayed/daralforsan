namespace Models
{
    public class SchoolFruitsDetails
    {
        public long ID { get; set; }
        public long FruitsID { get; set; }
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;

        public virtual SchoolFruits Fruits { get; set; } = new SchoolFruits();
    }
}