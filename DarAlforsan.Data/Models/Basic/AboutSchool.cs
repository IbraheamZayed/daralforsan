namespace Models
{
    public class AboutSchool
    {
        public long ID { get; set; }
        public string AddressAr { get; set; } = string.Empty;
        public string AddressEn { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string WhatsAppNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DetailsAr { get; set; } = string.Empty;
        public string DetailsEn { get; set; } = string.Empty;
        public float Lat { get; set; }
        public float Lng { get; set; }
        public string Logo { get; set; } = string.Empty;
        public string? RegisterationUrl { get; set; }
    }
}