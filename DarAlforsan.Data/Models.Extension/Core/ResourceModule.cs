using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class ResourceModule
    {
        public static Func<IQueryable<Models.ResourceModule>, IOrderedQueryable<Models.ResourceModule>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.ResourceModule, object>> Exp = null;
            switch (ColumnName)
            {
                case "ModuleID": Exp = obj => obj.ModuleID; break;
                case "Name": Exp = obj => obj.Name; break;
                case "LocalName": Exp = obj => obj.LocalName; break;
                case "LatinName": Exp = obj => obj.LatinName; break;
            }

            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}