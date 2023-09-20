using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class SchoolVision : Repository<Models.SchoolVision>, ISchoolVision
    {
        public SchoolVision(DBContext dBContext) : base(dBContext)
        {

        }
    }
}