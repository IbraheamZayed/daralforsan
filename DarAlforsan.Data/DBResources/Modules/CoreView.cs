//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class CoreView
    {
        //-----------------------------------------------------------------------------------------
        public static long ModuleID = 5;
        //-----------------------------------------------------------------------------------------
        public static string SelectValue
        {
            get
            {
                return ResourceManager.GetResourceByID(42,ModuleID);
            }
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////