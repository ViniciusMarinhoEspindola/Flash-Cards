using Domain.Entities.Generics;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Card : BaseEntity
    {
        public Guid UserLanguageId { get; private set; }
        public string Term { get; private set; } = string.Empty;
        public string Translation { get; private set; } = string.Empty;
        public string? Romanization { get; private set; }
        public bool IsPhrase { get; private set; } = false;
        public CardSource Source { get; private set; } = CardSource.Manual;

        public UserLanguage UserLanguage { get; private set; } = null!;
        public ICollection<CardExample> CardExamples { get; private set; } = [];
        public CardProgress? Progress { get; private set; } = null;

        protected Card() { }

        public static Card Create(Guid userLanguageId, string term, string translation, string? romanization = null, bool isPhrase = false, CardSource source = CardSource.Manual)
        {
            return new Card
            {
                UserLanguageId = userLanguageId,
                Term = term.Trim(),
                Translation = translation.Trim(),
                Romanization = romanization?.Trim(),
                IsPhrase = isPhrase,
                Source = source
            };
        }
    }
}
