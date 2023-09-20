namespace Models
{
    public class ThemeLogin
    {
        public long ThemeID { get; set; }
        public string ThemeName { get; set; }
        public bool IsDefault { get; set; }        
        public string BGImgPath { get; set; }
        public string LogoImgPath { get; set; }
        public string SideImgPath { get; set; }
        public string SideImgPadding { get; set; }
        public string LogoImgPadding { get; set; }
        public string ForeColor { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}