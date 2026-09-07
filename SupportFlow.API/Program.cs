using SupportFlow.API.Agents;
using SupportFlow.API.Events;
using SupportFlow.API.Executors;
using SupportFlow.API.Services;
using SupportFlow.API.Workflows;

var builder = WebApplication.CreateBuilder(args);

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
// SERVICES
// ==========================================

builder.Services.AddSingleton<MultiAgentService>();
builder.Services.AddSingleton<SupportWorkflowService>();

// ==========================================
// EXECUTORS
// ==========================================

builder.Services.AddSingleton<RequestExecutor>();
builder.Services.AddSingleton<CoordinatorExecutor>();

builder.Services.AddSingleton<SupportExecutor>();
builder.Services.AddSingleton<BillingExecutor>();
builder.Services.AddSingleton<TechnicalExecutor>();

builder.Services.AddSingleton<ResponseExecutor>();

// ==========================================
// WORKFLOW
// ==========================================

builder.Services.AddSingleton<SupportWorkflow>();

// Event logger 
builder.Services.AddSingleton<WorkflowEventLogger>();

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