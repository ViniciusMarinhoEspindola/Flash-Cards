using Domain.Entities.Generics;

namespace Domain.Entities
{
    public class Workspace : BaseEntity
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Guid? LanguageId { get; private set; }
        public Guid? NativeLanguageId { get; private set; }

        public User User { get; private set; } = null!;
        public Language? Language { get; private set; } = null;
        public Language? NativeLanguage { get; private set; } = null;
        public ICollection<Deck> Decks { get; private set; } = [];

        protected Workspace() { }

        public static Workspace Create(Guid userId, string name, Guid? languageId = null, Guid? nativeLanguageId = null)
        {
            return new Workspace
            {
                UserId = userId,
                Name = name.Trim(),
                LanguageId = languageId,
                NativeLanguageId = nativeLanguageId
            };
        }

        public void Update(string name)
        {
            Name = name.Trim();
        }
    }
}
