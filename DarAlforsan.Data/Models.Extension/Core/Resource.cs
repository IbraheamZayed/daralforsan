using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public partial class Resource
    {
        public static Func<IQueryable<Models.Resource>, IOrderedQueryable<Models.Resource>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.Resource, object>> Exp = null;
            switch (ColumnName)
            {
                case "ResourceID": Exp = obj => obj.ResourceID; break;
                case "Name": Exp = obj => obj.Name; break;
                case "LocalValue": Exp = obj => obj.LocalValue; break;
                case "LatinValue": Exp = obj => obj.LatinValue; break;
            }

            if (Exp == null)
            {
                Exp = GetOrderByExpression(ColumnName);
            }

            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}
