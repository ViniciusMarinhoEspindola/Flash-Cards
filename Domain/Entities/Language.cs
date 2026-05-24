using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Language : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public string? FlagEmoji { get; private set; }


        protected Language() { }

        public static Language Create(Guid userId, string name, string code, string? flagEmoji)
        {
            return new Language
            {
                Name = name.Trim(),
                Code = code.ToUpperInvariant().Trim(),
                FlagEmoji = flagEmoji?.Trim()
            };
        }
    }
}
