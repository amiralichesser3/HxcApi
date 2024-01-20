namespace HxcApi.DataAccess.Contracts.Common.Queries;

public interface IGetOrganizationTodosQueryGenerator
{
    string GenerateSelectAllQuery();
    string GenerateSelectByIdQuery(int id);
}