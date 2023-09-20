using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class SchoolFruits : Repository<Models.SchoolFruits>, ISchoolFruits
    {
        public SchoolFruits(DBContext dBContext) : base(dBContext)
        {

        }
    }
}