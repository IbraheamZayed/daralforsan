using Core;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal class User : Repository<Models.User>, IUser
    {
        public User(DbContext dBContext) : base(dBContext)
        {

        }
    }
}