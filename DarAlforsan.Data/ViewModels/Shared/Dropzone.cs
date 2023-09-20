namespace ViewModels.Shared
{
    public class Dropzone
    {
        public string ID { get; set; }
        public bool AjaxLoad { get; set; }
        public string MessageTile { get; set; }
        public string TargetControl { get; set; }
        public int? MaxFiles { get; set; }
        public string AcceptedFiles { get; set; }
        public string UploadUrl { get; set; }
    }
}