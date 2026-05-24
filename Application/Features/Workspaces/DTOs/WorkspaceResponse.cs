namespace Application.Features.Workspaces.DTOs
{
    public record WorkspaceResponse(
        Guid Id,
        string Name,
        Guid? LanguageId,
        string? LanguageName,
        string? LanguageCode,
        string? LanguageFlagEmoji,
        Guid? NativeLanguageId,
        string? NativeLanguageName,
        DateTime CreatedAt);
}
