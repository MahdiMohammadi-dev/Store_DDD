namespace Store.Persistence.Repositories;

using System.Linq.Expressions;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ExpressionOfSelectData();
}