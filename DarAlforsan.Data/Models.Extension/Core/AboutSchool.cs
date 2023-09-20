using Core.Data;
using System.Linq.Expressions;

namespace Models.Extensions
{
    public class AboutSchool
    {
        public static Func<IQueryable<Models.AboutSchool>, IOrderedQueryable<Models.AboutSchool>> GetOrderByFuncion(string ColumnName, string Dir)
        {
            Expression<Func<Models.AboutSchool, object>> Exp = null;
            switch (ColumnName)
            {
                case "ID": Exp = obj => obj.ID; break;
                case "AddressAr": Exp = obj => obj.AddressAr; break;
                case "AddressEn": Exp = obj => obj.AddressEn; break;
                case "MobileNo": Exp = obj => obj.MobileNo; break;
                case "WhatsAppNo": Exp = obj => obj.WhatsAppNo; break;
                case "Email": Exp = obj => obj.Email; break;
                case "Lat": Exp = obj => obj.Lat; break;
                case "Lng": Exp = obj => obj.Lng; break;
                case "DetailsAr": Exp = obj => obj.DetailsAr; break;
                case "DetailsEn": Exp = obj => obj.DetailsEn; break;
                case "RegisterationUrl": Exp = obj => obj.RegisterationUrl; break;
            }
            return obj => OrderDirection.Asc == Dir ? obj.OrderBy(Exp) : obj.OrderByDescending(Exp);
        }
    }
}