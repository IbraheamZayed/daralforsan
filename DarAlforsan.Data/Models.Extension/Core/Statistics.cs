using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class Statistics
    {
        public static Func<IQueryable<Models.Statistics>, IOrderedQueryable<Models.Statistics>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.Statistics, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "TitleAr": Exp = obj => obj.TitleAr; break;
                case "TitleEn": Exp = obj => obj.TitleEn; break;
                case "Count": Exp = obj => obj.Count; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}