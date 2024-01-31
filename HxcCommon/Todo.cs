namespace HxcCommon;

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false)
{
    public string? OrganizationId { get; set; }
}