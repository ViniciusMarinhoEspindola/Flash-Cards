using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Language
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;        // "zh", "ja", "en"
        public string NativeLanguageCode { get; private set; } = string.Empty; // idioma do usuário

        public User User { get; private set; } = null!;
        public ICollection<Card> Cards { get; private set; } = [];

        protected Language() { }

        public static Language Create(Guid userId, string name, string code, string nativeLanguageCode)
        {
            return new Language
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name.Trim(),
                Code = code.ToLowerInvariant().Trim(),
                NativeLanguageCode = nativeLanguageCode.ToLowerInvariant().Trim()
            };
        }
    }
}
