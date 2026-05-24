using Domain.Enums;

namespace Application.Features.Cards.DTOs
{
    public class CreateCardRequest
    {
        public Guid DeckId { get; set; }
        public string Term { get; set; } = string.Empty;
        public string Definition { get; set; } = string.Empty;
        public string? Romanization { get; set; }
        public bool IsPhrase { get; set; } = false;
        public CardSource Source { get; set; } = CardSource.Manual;
        public IEnumerable<CreateCardExampleRequest> Examples { get; set; } = [];
    }

    public class CreateCardExampleRequest
    {
        public string Sentence { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
