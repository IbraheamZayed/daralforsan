//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class DropZone
    {
        //-----------------------------------------------------------------------------------------
        public static long ModuleID = 7;
        //-----------------------------------------------------------------------------------------
        public static string FileTooBig
        {
            get
            {
                return ResourceManager.GetResourceByID(56,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string InvalidFileType
        {
            get
            {
                return ResourceManager.GetResourceByID(57,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string ResponseError
        {
            get
            {
                return ResourceManager.GetResourceByID(58,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CancelUpload
        {
            get
            {
                return ResourceManager.GetResourceByID(59,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CancelUploadConfirmation
        {
            get
            {
                return ResourceManager.GetResourceByID(60,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string RemoveFile
        {
            get
            {
                return ResourceManager.GetResourceByID(61,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string MaxFilesExceeded
        {
            get
            {
                return ResourceManager.GetResourceByID(62,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string DropFileHere
        {
            get
            {
                return ResourceManager.GetResourceByID(63,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string SuccesUploadMessage
        {
            get
            {
                return ResourceManager.GetResourceByID(64,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string ErrorUploadMessage
        {
            get
            {
                return ResourceManager.GetResourceByID(65,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////