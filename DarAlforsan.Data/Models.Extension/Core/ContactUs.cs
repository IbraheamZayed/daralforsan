using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class ContactUs
    {
        public static Func<IQueryable<Models.ContactUs>, IOrderedQueryable<Models.ContactUs>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.ContactUs, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "Name": Exp = obj => obj.Name; break;
                case "Email": Exp = obj => obj.Email; break;
                case "MobileNo": Exp = obj => obj.MobileNo; break;
                case "Message": Exp = obj => obj.Message; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}