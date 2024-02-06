namespace HxcApiClient.Todos.Records;

public record TodoRecord(int Id, string? Title, DateTime? DueBy = null, bool IsComplete = false);