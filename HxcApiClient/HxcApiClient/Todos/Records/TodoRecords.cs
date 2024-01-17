namespace HxcApiClient.Records;

public record TodoRecord(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);