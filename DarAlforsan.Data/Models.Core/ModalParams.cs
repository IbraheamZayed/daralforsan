using Core.UI;

namespace Models.Core
{
    public class ModalParams
    {
        public ModalParams()
        {
            Keyboard = true;
            BackDrop = true;
            Size = ModalSize.Medium;
        }

        public string ID { get; set; }
        public string BodyID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? Keyboard { get; set; }
        public bool? BackDrop { get; set; }
        public string Size { get; set; }
        public string ClassName { get; set; }
    }
}