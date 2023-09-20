//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class CoreCommand
    {
        //-----------------------------------------------------------------------------------------
        public static long ModuleID = 1;
        //-----------------------------------------------------------------------------------------
        public static string CmdSave
        {
            get
            {
                return ResourceManager.GetResourceByID(1,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdLogout
        {
            get
            {
                return ResourceManager.GetResourceByID(2,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdOk
        {
            get
            {
                return ResourceManager.GetResourceByID(3,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdCancel
        {
            get
            {
                return ResourceManager.GetResourceByID(4,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdLogin
        {
            get
            {
                return ResourceManager.GetResourceByID(5,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdEdit
        {
            get
            {
                return ResourceManager.GetResourceByID(6,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdDelete
        {
            get
            {
                return ResourceManager.GetResourceByID(7,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdSend
        {
            get
            {
                return ResourceManager.GetResourceByID(8,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
        public static string CmdAdd
        {
            get
            {
                return ResourceManager.GetResourceByID(9,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////