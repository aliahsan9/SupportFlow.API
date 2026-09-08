using Microsoft.EntityFrameworkCore;
using SupportFlow.API.Models;

namespace SupportFlow.API.Data;

public sealed class SupportFlowDbContext : DbContext
{
    public SupportFlowDbContext(
        DbContextOptions<SupportFlowDbContext> options)
        : base(options)
    {
    }

    // ==========================================
    // PERSISTENT MEMORY
    // ==========================================

    public DbSet<AgentMemory> AgentMemories =>
        Set<AgentMemory>();

    // ==========================================
    // KNOWLEDGE BASE
    // ==========================================

    public DbSet<KnowledgeDocument> KnowledgeDocuments =>
        Set<KnowledgeDocument>();

    public DbSet<KnowledgeChunk> KnowledgeChunks =>
        Set<KnowledgeChunk>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ==========================================
        // AGENT MEMORY
        // ==========================================

        modelBuilder.Entity<AgentMemory>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ConversationId)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Value)
                .IsRequired();

            entity.Property(x => x.Category)
                .HasMaxLength(100);

            entity.HasIndex(x => new
            {
                x.ConversationId,
                x.Key
            });
        });

        // ==========================================
        // KNOWLEDGE DOCUMENT
        // ==========================================

        modelBuilder.Entity<KnowledgeDocument>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);

            entity.Property(x => x.Source)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(x => x.Category)
                .HasMaxLength(100);

            entity.HasMany(x => x.Chunks)
                .WithOne(x => x.Document)
                .HasForeignKey(x => x.KnowledgeDocumentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // ==========================================
        // KNOWLEDGE CHUNK
        // ==========================================

        modelBuilder.Entity<KnowledgeChunk>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Content)
                .IsRequired();

            entity.Property(x => x.Embedding)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.KnowledgeDocumentId,
                x.ChunkIndex
            });
        });
    }
}