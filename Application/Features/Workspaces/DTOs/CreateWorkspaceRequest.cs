namespace Application.Features.Workspaces.DTOs
{
    public class CreateWorkspaceRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid? LanguageId { get; set; }
        public Guid? NativeLanguageId { get; set; }
    }
}
