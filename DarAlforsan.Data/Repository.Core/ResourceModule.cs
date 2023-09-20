using Core;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class ResourceModule : Repository<Models.ResourceModule>, IResourceModule
    {
        public ResourceModule(DbContext dBContext) :base(dBContext)
        {

        }
    }
}