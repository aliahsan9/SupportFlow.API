namespace SupportFlow.API.Models;

public sealed class KnowledgeDocument
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Source { get; set; } = string.Empty;

    public string? Category { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public ICollection<KnowledgeChunk> Chunks { get; set; }
        = new List<KnowledgeChunk>();
}