using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class Statistics : Repository<Models.Statistics>, IStatistics
    {
        public Statistics(DBContext dBContext) : base(dBContext)
        {

        }
    }
}