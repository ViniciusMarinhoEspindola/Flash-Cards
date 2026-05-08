using Domain.Entities.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string? RefreshToken { get; private set; }
        public DateTime? RefreshTokenExpiresAt { get; private set; }

        public ICollection<UserLanguage> UserLanguages { get; private set; } = [];

        protected User() { }

        public static User Create(string email, string name, string passwordHash)
        {
            return new User
            {
                Email = email.ToLowerInvariant().Trim(),
                Name = name.Trim(),
                PasswordHash = passwordHash
            };
        }

        public void SetRefreshToken(string token, DateTime expiresAt)
        {
            RefreshToken = token;
            RefreshTokenExpiresAt = expiresAt;
        }

        public void RevokeRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiresAt = null;
        }
    }
}
