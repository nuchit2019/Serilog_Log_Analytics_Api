using Serilog;
using LogAnalytics.Client;
using LogAnalyticsApi; 


var builder = WebApplication.CreateBuilder(args);

//=====================================================//
// กำหนดค่า Azure Log Analytics
var logAnalyticsWorkspaceId = builder.Configuration["LogAnalytics:WorkspaceId"];
var logAnalyticsSharedKey = builder.Configuration["LogAnalytics:SharedKey"];

// สร้าง client สำหรับ LogAnalytics
var logAnalyticsClient = new LogAnalyticsClient(logAnalyticsWorkspaceId, logAnalyticsSharedKey);

// ตั้งค่า Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()  // Log ลง Console
    .WriteTo.Sink(new LogAnalyticsSink(logAnalyticsClient))  // Log ไปที่ Log Analytics
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

//=====================================================//

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// สร้าง Web Application
var app = builder.Build();

// ตั้งค่า Middleware 
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

// เริ่มการทำงานของแอป
app.Run();
