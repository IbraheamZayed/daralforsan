using Core;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class Resource : Repository<Models.Resource>, IResource
    {
        public Resource(DbContext dBContext) :base(dBContext)
        {

        }
    }
}