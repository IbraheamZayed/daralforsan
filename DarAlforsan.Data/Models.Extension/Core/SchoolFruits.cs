using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class SchoolFruits
    {
        public static Func<IQueryable<Models.SchoolFruits>, IOrderedQueryable<Models.SchoolFruits>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.SchoolFruits, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "TitleAr": Exp = obj => obj.TitleAr; break;
                case "TitleEn": Exp = obj => obj.TitleEn; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}