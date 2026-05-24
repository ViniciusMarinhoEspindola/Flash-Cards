namespace Application.Features.Cards.DTOs
{
    public class CaptureTermRequest
    {
        public Guid DeckId { get; set; }
        public string Term { get; set; } = string.Empty;
    }
}
