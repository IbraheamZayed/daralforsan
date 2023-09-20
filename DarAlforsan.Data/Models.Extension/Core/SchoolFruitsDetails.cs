using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public partial class SchoolFruitsDetails
    {
        public static Func<IQueryable<Models.SchoolFruitsDetails>, IOrderedQueryable<Models.SchoolFruitsDetails>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.SchoolFruitsDetails, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "TitleAr": Exp = obj => obj.TitleAr; break;
                case "TitleEn": Exp = obj => obj.TitleEn; break;
            }

            if (Exp == null)
            {
                Exp = GetOrderByExpression(ColumnName);
            }

            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}