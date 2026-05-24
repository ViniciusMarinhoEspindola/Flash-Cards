namespace Application.Features.Languages.DTOs
{
    public record LanguageResponse(Guid Id, string Name, string Code, string? FlagEmoji);
}
