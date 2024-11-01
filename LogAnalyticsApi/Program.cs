using Serilog;
using LogAnalytics.Client;
using LogAnalyticsApi; 


var builder = WebApplication.CreateBuilder(args);

//=====================================================//
// ��˹���� Azure Log Analytics
var logAnalyticsWorkspaceId = builder.Configuration["LogAnalytics:WorkspaceId"];
var logAnalyticsSharedKey = builder.Configuration["LogAnalytics:SharedKey"];

// ���ҧ client ����Ѻ LogAnalytics
var logAnalyticsClient = new LogAnalyticsClient(logAnalyticsWorkspaceId, logAnalyticsSharedKey);

// ��駤�� Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Log ŧ Console
    .WriteTo.Sink(new LogAnalyticsSink(logAnalyticsClient))  // Log 价�� Log Analytics
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

//=====================================================//

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ���ҧ Web Application
var app = builder.Build();

// ��駤�� Middleware 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// �������÷ӧҹ�ͧ�ͻ
app.Run();
