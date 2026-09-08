using Microsoft.EntityFrameworkCore;
using SupportFlow.API.Agents;
using SupportFlow.API.Data;
using SupportFlow.API.Events;
using SupportFlow.API.Executors;
using SupportFlow.API.Services;
using SupportFlow.API.Workflows;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// DATABASE
// ==========================================

builder.Services.AddDbContext<SupportFlowDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

// ==========================================
// CONTROLLERS / SWAGGER
// ==========================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ==========================================
// AGENTS
// ==========================================

builder.Services.AddSingleton<TicketService>();

builder.Services.AddSingleton<CoordinatorAgent>();
builder.Services.AddSingleton<SupportAgent>();
builder.Services.AddSingleton<BillingAgent>();
builder.Services.AddSingleton<TechnicalAgent>();

// ==========================================
// APPLICATION SERVICES
// ==========================================

builder.Services.AddSingleton<MultiAgentService>();

// ==========================================
// PERSISTENT MEMORY
// ==========================================

// AgentMemoryService uses SupportFlowDbContext,
// therefore it must remain Scoped.

builder.Services.AddScoped<AgentMemoryService>();

// ==========================================
// HUMAN-IN-THE-LOOP
// ==========================================

// Uses in-memory state for Milestone 11.

builder.Services.AddSingleton<HumanApprovalService>();

// ==========================================
// WORKFLOW EVENTS
// ==========================================

builder.Services.AddSingleton<WorkflowEventLogger>();

// ==========================================
// EXECUTORS
// ==========================================

// Workflow-related components are Scoped so that
// future scoped services such as Memory/RAG can safely
// be injected into them.

builder.Services.AddScoped<RequestExecutor>();

builder.Services.AddScoped<CoordinatorExecutor>();

builder.Services.AddScoped<SupportExecutor>();

builder.Services.AddScoped<BillingExecutor>();

builder.Services.AddScoped<TechnicalExecutor>();

builder.Services.AddScoped<ResponseExecutor>();

// ==========================================
// WORKFLOW
// ==========================================

builder.Services.AddScoped<SupportWorkflow>();

builder.Services.AddScoped<SupportWorkflowService>();

// ==========================================
// APPLICATION
// ==========================================

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();