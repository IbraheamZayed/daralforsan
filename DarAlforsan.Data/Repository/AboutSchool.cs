using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class AboutSchool : Repository<Models.AboutSchool>, IAboutSchool
    {
        public AboutSchool(DBContext dBContext) : base(dBContext)
        {

        }
    }
}