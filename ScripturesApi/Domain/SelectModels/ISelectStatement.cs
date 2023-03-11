using System.Linq.Expressions;
using ScripturesApi.Domain.Entities;

namespace ScripturesApi.Domain.SelectModels;

public interface ISelectStatement<T, TResult> where T : EntityBase
{
    public Expression<Func<T, TResult>> SelectStatement { get; }
}
