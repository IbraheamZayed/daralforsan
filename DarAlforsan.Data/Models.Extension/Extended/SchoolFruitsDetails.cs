using System.Linq.Expressions;

namespace Models.Extensions
{
    public partial class SchoolFruitsDetails
    {
        protected static Expression<Func<Models.SchoolFruitsDetails, object>> GetOrderByExpression(string ColumnName)
        {
            Expression<Func<Models.SchoolFruitsDetails, object>> Exp = null;
            switch (ColumnName)
            {
                case "FruitsTitleAr": Exp = obj => obj.Fruits.TitleAr; break;
                case "FruitsTitleEn": Exp = obj => obj.Fruits.TitleEn; break;
            }
            return Exp;
        }
    }
}