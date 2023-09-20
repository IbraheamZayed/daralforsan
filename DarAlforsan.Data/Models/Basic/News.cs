namespace Models
{
    public class News
    {
        public long ID { get; set; }
        public string TitleAr { get; set; } = string.Empty;
        public string TitleEn { get; set; } = string.Empty;
        public string DetailsAr { get; set; } = string.Empty;
        public string DetailsEn { get; set; } = string.Empty;
        public string MainImg { get; set; } = string.Empty;
        public string Imgs { get; set; } = string.Empty;
        public int ViewsCount { get; set; }
        public DateTime? AddDate { get; set; }
    }
}