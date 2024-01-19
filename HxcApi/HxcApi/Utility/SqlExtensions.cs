using HxcCommon;
using Microsoft.Data.SqlClient;

namespace HxcApi.Utility;

public static class SqlExtensions
{
    public static IEnumerable<Todo> GetTodoData(this SqlConnection connection, string commandText)
    {
        List<Todo> todoList = new List<Todo>();

        using SqlCommand command = new SqlCommand(commandText, connection);
        connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Todo todo = new Todo(Convert.ToInt32(reader[nameof(Todo.Id)]), reader[nameof(Todo.Title)].ToString(),
                reader[nameof(Todo.DueBy)] is DBNull ? null : ((DateTime)reader[nameof(Todo.DueBy)]).ToDateOnly(),
                Convert.ToBoolean(reader[nameof(Todo.IsComplete)]));
            todoList.Add(todo);
        }

        connection.Close();

        return todoList;
    }
}