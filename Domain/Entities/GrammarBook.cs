using Domain.Entities.Generics;

namespace Domain.Entities
{
    // Fase 4 stub
    public class GrammarBook : BaseEntity
    {
        public Guid WorkspaceId { get; private set; }

        protected GrammarBook() { }

        public static GrammarBook Create(Guid workspaceId)
        {
            return new GrammarBook { WorkspaceId = workspaceId };
        }
    }
}
