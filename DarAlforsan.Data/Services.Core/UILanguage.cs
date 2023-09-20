using System.Collections.Generic;
using System.Linq;
using Core;

//\////////////////////////////////////////////////////////////////////////////////////////////////
namespace Services
{
    //\////////////////////////////////////////////////////////////////////////////////////////////
    public class UILanguage : Service<UnitOfWork>
    {
        //-----------------------------------------------------------------------------------------
        public UILanguage()
        {
        }
        //-----------------------------------------------------------------------------------------
        public string GetLangugeDir(long LanguageID)
        {
            return unitOfWork.UILanguages.FindFromCache(ui => ui.LanguageID == LanguageID).Direction;
        }
        //-----------------------------------------------------------------------------------------
        public ViewModels.UILanguage GetDefault()
        {
            IEnumerable<ViewModels.UILanguage> UILangs = unitOfWork.UILanguages.Where(ui => ui.IsDefault == true, UI => new ViewModels.UILanguage { LanguageID = UI.LanguageID, Name = UI.Name, LocalName = UI.LocalName, LatinName = UI.LatinName, Direction = UI.Direction, ISOLanguageName = UI.ISOLanguageName });
            if (UILangs.Count() == 1)
            {
                return UILangs.First();
            }
            return null;
        }
        //-----------------------------------------------------------------------------------------
        public ViewModels.UILanguage GetByID(long LanguageID)
        {
            IEnumerable<ViewModels.UILanguage> UILangs = unitOfWork.UILanguages.Where(ui => ui.LanguageID == LanguageID, UI => new ViewModels.UILanguage { LanguageID = UI.LanguageID, Name = UI.Name, LocalName = UI.LocalName, LatinName = UI.LatinName, Direction = UI.Direction, ISOLanguageName = UI.ISOLanguageName });
            if (UILangs.Count() == 1)
            {
                return UILangs.First();
            }
            return null;
        }
        //-----------------------------------------------------------------------------------------
    }
    //\////////////////////////////////////////////////////////////////////////////////////////////
}
//\////////////////////////////////////////////////////////////////////////////////////////////////
