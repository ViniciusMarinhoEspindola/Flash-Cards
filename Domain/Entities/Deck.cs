using Domain.Entities.Generics;

namespace Domain.Entities
{
    public class Deck : BaseEntity
    {
        public Guid WorkspaceId { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public Workspace Workspace { get; private set; } = null!;
        public ICollection<Card> Cards { get; private set; } = [];

        protected Deck() { }

        public static Deck Create(Guid workspaceId, string name)
        {
            return new Deck
            {
                WorkspaceId = workspaceId,
                Name = name.Trim()
            };
        }
    }
}
