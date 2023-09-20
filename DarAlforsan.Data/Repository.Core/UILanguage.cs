using Core;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class UILanguage : Repository<Models.UILanguage>, IUILanguage
    {
        public UILanguage(DbContext DBContext) : base(DBContext)
        {

        }
    }
}