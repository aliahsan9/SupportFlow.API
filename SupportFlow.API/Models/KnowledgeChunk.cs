namespace SupportFlow.API.Models;

public sealed class KnowledgeChunk
{
    public Guid Id { get; set; }

    public Guid KnowledgeDocumentId { get; set; }

    public string Content { get; set; } = string.Empty;

    public string Embedding { get; set; } = string.Empty;

    public int ChunkIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public KnowledgeDocument Document { get; set; } = null!;
}