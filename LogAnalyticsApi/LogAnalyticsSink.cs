using LogAnalytics.Client;
using LogAnalyticsApi.Models;
using Serilog.Core;
using Serilog.Events;

namespace LogAnalyticsApi
{
    public class LogAnalyticsSink : ILogEventSink
    {
        private readonly LogAnalyticsClient _client;

        public LogAnalyticsSink(LogAnalyticsClient client)
        {
            _client = client;
        }

        public async void Emit(LogEvent logEvent)
        {

            //var logData = new LogsModel
            //{
            //    Level = logEvent.Level.ToString(),
            //    Message = logEvent.RenderMessage(),
            //    Exception = logEvent.Exception?.ToString()
            //};
            //_client.SendLogEntry(logData, "MyCustomLogType").Wait();


            // กรองเฉพาะ log ที่เป็นระดับ Error หรือสูงกว่า (เช่น Fatal)
            //if (logEvent.Level >= LogEventLevel.Error)
            if (logEvent.Level == LogEventLevel.Error ||
                logEvent.Level == LogEventLevel.Fatal ||
                logEvent.Level == LogEventLevel.Warning)
            {
                var logData = new LogsModel
                {
                    Level = logEvent.Level.ToString(),
                    Message = logEvent.RenderMessage(),
                    Exception = logEvent.Exception?.ToString()
                };

                // ส่งข้อมูล log ไปยัง Log Analytics
                _client.SendLogEntry(logData, "MyCustomLogType").Wait();
            }

        }
    }
}
