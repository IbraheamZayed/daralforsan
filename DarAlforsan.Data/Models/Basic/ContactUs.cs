namespace Models
{
    public class ContactUs
    {
        public long ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MobileNo { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty; 
    }
}