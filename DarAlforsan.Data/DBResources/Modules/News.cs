//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class News
    {
        //-----------------------------------------------------------------------------------------
        public static long ModuleID = 16;
        //-----------------------------------------------------------------------------------------
        public static string PageTitle
        {
            get
            {
                return ResourceManager.GetResourceByID(115,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Title
        {
            get
            {
                return ResourceManager.GetResourceByID(117,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string TitleAr
        {
            get
            {
                return ResourceManager.GetResourceByID(118,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string TitleEn
        {
            get
            {
                return ResourceManager.GetResourceByID(119,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Details
        {
            get
            {
                return ResourceManager.GetResourceByID(120,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string ViewsCount
        {
            get
            {
                return ResourceManager.GetResourceByID(121,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string AddDate
        {
            get
            {
                return ResourceManager.GetResourceByID(122,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string MainImg
        {
            get
            {
                return ResourceManager.GetResourceByID(123, ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Imgs
        {
            get
            {
                return ResourceManager.GetResourceByID(124, ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////