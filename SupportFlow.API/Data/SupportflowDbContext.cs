using Microsoft.Agents.AI;
using Microsoft.EntityFrameworkCore;
using SupportFlow.API.Models;

namespace SupportFlow.API.Data;

public class SupportFlowDbContext : DbContext
{
    public SupportFlowDbContext(
        DbContextOptions<SupportFlowDbContext> options)
        : base(options)
    {
    }

    public DbSet<AgentMemory> AgentMemories => Set<AgentMemory>();
protected override void OnModelCreating(
    ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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
    }
}