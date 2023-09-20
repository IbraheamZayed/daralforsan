using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class SchoolSystem : Repository<Models.SchoolSystem>, ISchoolSystem
    {
        public SchoolSystem(DBContext dBContext) : base(dBContext)
        {

        }
    }
}