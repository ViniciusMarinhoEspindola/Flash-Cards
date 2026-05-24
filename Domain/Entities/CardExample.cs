using Domain.Entities.Generics;

namespace Domain.Entities
{
    public class CardExample : BaseEntity
    {
        public Guid CardId { get; private set; }
        public string Sentence { get; private set; } = string.Empty;
        public string Note { get; private set; } = string.Empty;

        public Card Card { get; private set; } = null!;

        protected CardExample() { }

        public static CardExample Create(Guid cardId, string sentence, string note)
        {
            return new CardExample
            {
                CardId = cardId,
                Sentence = sentence.Trim(),
                Note = note.Trim()
            };
        }
    }
}
