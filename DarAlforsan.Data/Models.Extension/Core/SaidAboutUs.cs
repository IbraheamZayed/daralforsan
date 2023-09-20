using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class SaidAboutUs
    {
        public static Func<IQueryable<Models.SaidAboutUs>, IOrderedQueryable<Models.SaidAboutUs>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.SaidAboutUs, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "Name": Exp = obj => obj.Name; break;
                case "MessageAr": Exp = obj => obj.MessageAr; break;
                case "MessageEn": Exp = obj => obj.MessageEn; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}