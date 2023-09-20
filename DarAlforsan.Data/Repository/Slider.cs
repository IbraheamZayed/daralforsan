using Core;
using DarAlforsan.Data.Core;

namespace Repository
{
    internal class Slider : Repository<Models.Slider>, ISlider
    {
        public Slider(DBContext dBContext) : base(dBContext)
        {

        }
    }
}