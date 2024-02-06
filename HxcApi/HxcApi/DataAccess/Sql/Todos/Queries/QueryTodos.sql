-- name: GetTodos
-- Query Todos
SELECT Id, Title, DueBy, IsComplete
FROM Todos;

-- name: GetTodosById
-- Query Todos by Id
SELECT Id, Title, DueBy, IsComplete
FROM Todos WHERE Id = @id;

-- name: InsertTodo
-- Insert Todo
INSERT INTO Todos (Title, DueBy, IsComplete, OrganizationId)
VALUES (@title, @dueBy, @isComplete, @organizationId);