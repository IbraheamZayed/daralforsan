using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class News
    {
        public static Func<IQueryable<Models.News>, IOrderedQueryable<Models.News>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.News, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "TitleAr": Exp = obj => obj.TitleAr; break;
                case "TitleEn": Exp = obj => obj.TitleEn; break;
                case "DetailsAr": Exp = obj => obj.DetailsAr; break;
                case "DetailsEn": Exp = obj => obj.DetailsEn; break;
                case "ViewsCount": Exp = obj => obj.ViewsCount; break;
                case "AddDate": Exp = obj => obj.AddDate; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}