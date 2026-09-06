using SupportFlow.API.Agents;
using SupportFlow.API.Executors;
using SupportFlow.API.Services;
using SupportFlow.API.Workflows;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<SupportAgent>();
builder.Services.AddSingleton<TicketService>();
builder.Services.AddSingleton<CoordinatorAgent>();
builder.Services.AddSingleton<SupportAgent>();
builder.Services.AddSingleton<BillingAgent>();
builder.Services.AddSingleton<TechnicalAgent>();

builder.Services.AddSingleton<MultiAgentService>();
builder.Services.AddSingleton<SupportWorkflowService>();

builder.Services.AddSingleton<RequestExecutor>();
builder.Services.AddSingleton<CoordinatorExecutor>();
builder.Services.AddSingleton<ResponseExecutor>();

builder.Services.AddSingleton<SupportWorkflow>();
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