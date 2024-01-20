using HxcApi.DataAccess.Contracts.Common.Queries;

namespace HxcApi.DataAccess.DapperImplementation.Todos.Queries;

public class GetOrganizationTodosQueryGenerator : IGetOrganizationTodosQueryGenerator
{
    public string GenerateSelectAllQuery()
    {
        return """
               SELECT Id, Title, DueBy, IsComplete
                                                    FROM Todos;
               """;
    }
    
    public string GenerateSelectByIdQuery(int id)
    {
        return $"""
               SELECT Id, Title, DueBy, IsComplete
                                                    FROM Todos WHERE Id = {id};
               """;
    }
}