using Microsoft.EntityFrameworkCore;
using SupportFlow.API.Data;
using SupportFlow.API.Models;

namespace SupportFlow.API.Services;

public sealed class AgentMemoryService
{
    private readonly SupportFlowDbContext _db;

    public AgentMemoryService(
        SupportFlowDbContext db)
    {
        _db = db;
    }

    public async Task<List<AgentMemory>> GetMemoriesAsync(
        string conversationId,
        CancellationToken cancellationToken = default)
    {
        return await _db.AgentMemories
            .Where(x => x.ConversationId == conversationId)
            .OrderByDescending(x => x.UpdatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<AgentMemory?> GetMemoryAsync(
        string conversationId,
        string key,
        CancellationToken cancellationToken = default)
    {
        return await _db.AgentMemories
            .FirstOrDefaultAsync(
                x =>
                    x.ConversationId == conversationId &&
                    x.Key == key,
                cancellationToken);
    }

    public async Task<AgentMemory> SaveMemoryAsync(
        string conversationId,
        string key,
        string value,
        string? category = null,
        CancellationToken cancellationToken = default)
    {
        var memory = await GetMemoryAsync(
            conversationId,
            key,
            cancellationToken);

        if (memory is null)
        {
            memory = new AgentMemory
            {
                Id = Guid.NewGuid(),
                ConversationId = conversationId,
                Key = key,
                Value = value,
                Category = category,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.AgentMemories.Add(memory);
        }
        else
        {
            memory.Value = value;
            memory.Category = category;
            memory.UpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync(cancellationToken);

        return memory;
    }

    public async Task<bool> DeleteMemoryAsync(
        string conversationId,
        string key,
        CancellationToken cancellationToken = default)
    {
        var memory = await GetMemoryAsync(
            conversationId,
            key,
            cancellationToken);

        if (memory is null)
        {
            return false;
        }

        _db.AgentMemories.Remove(memory);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}