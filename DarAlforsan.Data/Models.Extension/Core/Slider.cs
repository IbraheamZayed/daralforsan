using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class Slider
    {
        public static Func<IQueryable<Models.Slider>, IOrderedQueryable<Models.Slider>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.Slider, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "TitleAr": Exp = obj => obj.TitleAr; break;
                case "TitleEn": Exp = obj => obj.TitleEn; break;
                case "DetailsAr": Exp = obj => obj.DetailsAr; break;
                case "DetailsEn": Exp = obj => obj.DetailsEn; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}