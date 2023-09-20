using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class News : Repository<Models.News>, INews
    {
        public News(DBContext dBContext) : base(dBContext)
        {

        }
    }
}