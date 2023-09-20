//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class Resources
    {
        //-----------------------------------------------------------------------------------------
        public static long ModuleID = 12;
        //-----------------------------------------------------------------------------------------
        public static string ResourceModulePageTitle
        {
            get
            {
                return ResourceManager.GetResourceByID(84,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string LocalName
        {
            get
            {
                return ResourceManager.GetResourceByID(85,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string LatinName
        {
            get
            {
                return ResourceManager.GetResourceByID(86,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string AddEdit
        {
            get
            {
                return ResourceManager.GetResourceByID(87,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Name
        {
            get
            {
                return ResourceManager.GetResourceByID(88,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string ResourcesPageTitle
        {
            get
            {
                return ResourceManager.GetResourceByID(89,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Module
        {
            get
            {
                return ResourceManager.GetResourceByID(90,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////