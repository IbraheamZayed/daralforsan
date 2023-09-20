using System.Linq.Expressions;

namespace Models.Extensions
{
    public partial class Resource
    {
        protected static Expression<Func<Models.Resource, object>> GetOrderByExpression(string ColumnName)
        {
            Expression<Func<Models.Resource, object>> Exp = null;
            switch (ColumnName)
            {
                case "ModuleName": Exp = obj => obj.ResourceModule.Name; break;
                default: Exp = null; break;
            }
            return Exp;
        }
    }
}