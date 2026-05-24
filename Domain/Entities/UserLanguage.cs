using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserLanguage : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid LanguageId { get; private set; }
        public Guid NativeLanguageId { get; private set; }

        public User User { get; private set; } = null!;
        public Language Language { get; private set; } = null!;
        public Language NativeLanguage { get; private set; } = null!;

        public ICollection<Card> Cards { get; private set; } = [];

        protected UserLanguage() { }

        public static UserLanguage Create(Guid userId, Guid languageId, Guid nativeLanguageId)
        {
            return new UserLanguage
            {
                UserId = userId,
                LanguageId = languageId,
                NativeLanguageId = nativeLanguageId
            };
        }
    }
}
