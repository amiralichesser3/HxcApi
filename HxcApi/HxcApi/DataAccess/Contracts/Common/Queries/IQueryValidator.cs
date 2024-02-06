namespace HxcApi.DataAccess.Contracts.Common.Queries;

public interface IQueryValidator<TQuery>
    where TQuery : IQuery
{
    Task ValidateAsync(TQuery query);
}