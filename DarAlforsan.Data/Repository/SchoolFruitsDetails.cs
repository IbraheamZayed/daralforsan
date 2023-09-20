using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class SchoolFruitsDetails : Repository<Models.SchoolFruitsDetails>, ISchoolFruitsDetails
    {
        public SchoolFruitsDetails(DBContext dBContext) : base(dBContext)
        {

        }
    }
}