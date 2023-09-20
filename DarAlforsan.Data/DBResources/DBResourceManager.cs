using Core;
//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace DBResources
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class ResourceManager
    {
        //----------------------------------------------------------------------------------------
        public static string GetResourceByName(string ResourceName, string IsoName=Core.UI.UILanguage.GlobalIsoLocalName)
        {
            Resource resources = new Resource();
            return resources.GetResourceByName(ResourceName, IsoName);
        }
        //----------------------------------------------------------------------------------------
        public static string GetResourceByID(long ID, long ModuleID)
        {
            Resource resources = new Resource();
            return resources.GetResourceByID(ID, ModuleID);
        }
        //----------------------------------------------------------------------------------------
        public static string GetResourceByID(long ID, string IsoName = Core.UI.UILanguage.GlobalIsoLocalName)
        {
            Resource resources = new Resource();
            return resources.GetResourceByID(ID, IsoName);
        }
        //----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
    internal class Resource : Service<UnitOfWork>
    {
        //----------------------------------------------------------------------------------------
        public string GetResourceByName(string ResourceName, string IsoName = Core.UI.UILanguage.GlobalIsoLocalName)
        {
            var resource = unitOfWork.Resources.FindFromCache(r => r.Name == ResourceName);
            return string.Compare(IsoName, Core.UI.UILanguage.GlobalIsoLocalName, true) == 0 ? resource.LocalValue : resource.LatinValue;
        }
        //----------------------------------------------------------------------------------------
        public string GetResourceByID(long ID, string IsoName = Core.UI.UILanguage.GlobalIsoLocalName)
        {
            var resource = unitOfWork.Resources.FindFromCache(r => r.ResourceID == ID);
            return string.Compare(IsoName, Core.UI.UILanguage.GlobalIsoLocalName, true) == 0 ? resource.LocalValue : resource.LatinValue;
        }
        //----------------------------------------------------------------------------------------
        public string GetResourceByID(long ID, long ModuleID)
        {
            var resource = unitOfWork.Resources.FindFromCache(r => r.ResourceID == ID && r.ModuleID == ModuleID);

            return Core.UI.Locals.IsLocal ? resource.LocalValue : resource.LatinValue;
        }
        //----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////