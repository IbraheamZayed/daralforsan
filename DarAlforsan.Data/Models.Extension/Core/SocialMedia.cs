using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class SocialMedia
    {
        public static Func<IQueryable<Models.SocialMedia>, IOrderedQueryable<Models.SocialMedia>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.SocialMedia, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "Name": Exp = obj => obj.Name; break;
                case "Url": Exp = obj => obj.Url; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}