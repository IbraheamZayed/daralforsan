using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class SaidAboutUs : Repository<Models.SaidAboutUs>, ISaidAboutUs
    {
        public SaidAboutUs(DBContext dBContext) : base(dBContext)
        {

        }
    }
}