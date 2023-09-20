using Core;
using Microsoft.EntityFrameworkCore;

//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class SchoolVision
    {
        //-----------------------------------------------------------------------------------------
        public static long ModuleID = 20;
        //-----------------------------------------------------------------------------------------
        public static string PageTitle
        {
            get
            {
                return ResourceManager.GetResourceByID(139,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string TitleAr
        {
            get
            {
                return ResourceManager.GetResourceByID(140,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string TitleEn
        {
            get
            {
                return ResourceManager.GetResourceByID(141,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Title
        {
            get
            {
                return ResourceManager.GetResourceByID(142,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string Message
        {
            get
            {
                return ResourceManager.GetResourceByID(143,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////