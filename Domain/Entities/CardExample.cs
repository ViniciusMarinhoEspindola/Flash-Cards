using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CardExample : BaseEntity
    {
        public Guid CardId { get; private set; }
        public string Sentence { get; private set; } = string.Empty;
        public string Translation { get; private set; } = string.Empty;

        public Card Card { get; private set; } = null!;

        protected CardExample() { }

        public static CardExample Create(Guid cardId, string sentence, string translation)
        {
            return new CardExample
            {
                CardId = cardId,
                Sentence = sentence.Trim(),
                Translation = translation.Trim()
            };
        }
    }
}
