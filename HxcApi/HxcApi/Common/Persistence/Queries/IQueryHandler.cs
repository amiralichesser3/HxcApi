namespace HxcApi.Common.Persistence.Queries;

public interface IQueryHandler<T, TY> where T : IQuery<TY>
{
    Task<TY> Handle(T query);
}