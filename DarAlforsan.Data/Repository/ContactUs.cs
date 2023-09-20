using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class ContactUs : Repository<Models.ContactUs>, IContactUs
    {
        public ContactUs(DBContext dBContext) : base(dBContext)
        {

        }
    }
}