namespace HxcApi.Common.Persistence.Queries;

public interface IQueryValidator<TQuery>
    where TQuery : IQuery
{
    Task ValidateAsync(TQuery query);
}