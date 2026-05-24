using Domain.Entities.Generics;
using Domain.Enums;

namespace Domain.Entities
{
    public class Card : BaseEntity
    {
        public Guid DeckId { get; private set; }
        public string Term { get; private set; } = string.Empty;
        public string Definition { get; private set; } = string.Empty;
        public string? Romanization { get; private set; }
        public bool IsPhrase { get; private set; } = false;
        public CardSource Source { get; private set; } = CardSource.Manual;

        public Deck Deck { get; private set; } = null!;
        public ICollection<CardExample> CardExamples { get; private set; } = [];
        public CardProgress? Progress { get; private set; } = null;

        protected Card() { }

        public static Card Create(Guid deckId, string term, string definition, string? romanization = null, bool isPhrase = false, CardSource source = CardSource.Manual)
        {
            return new Card
            {
                DeckId = deckId,
                Term = term.Trim(),
                Definition = definition.Trim(),
                Romanization = romanization?.Trim(),
                IsPhrase = isPhrase,
                Source = source
            };
        }
    }
}
