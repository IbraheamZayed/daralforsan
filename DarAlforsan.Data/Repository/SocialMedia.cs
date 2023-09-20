using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class SocialMedia : Repository<Models.SocialMedia>, ISocialMedia
    {
        public SocialMedia(DBContext dBContext) : base(dBContext)
        {

        }
    }
}