using LMS.Domain.Entity;

using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LMS.API.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _rabbitMqConnectionString;
        private readonly string _queueName;

        public LoggingMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _rabbitMqConnectionString = configuration["RabbitMQ:ConnectionString"];
            _queueName = configuration["RabbitMQ:QueueName"] ?? "LMS_QUE";
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var combinedData = new
            {
                Method = context.Request.Method,
                Path = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                Headers = context.Request.Headers,
                Timestamp = DateTime.Now
            };
            var logEntry = new ExamLogs
            {
                Message = JsonSerializer.Serialize(combinedData),
                DateTime = DateTime.Now
            };
            SendToRabbitMq(_queueName, logEntry, _rabbitMqConnectionString);
            await _next(context);
        }

        private static void SendToRabbitMq(string queueName, object data, string connectionString)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(connectionString)
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonSerializer.Serialize(data);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: queueName,
                    basicProperties: null,
                    body: body);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Ошибка при отправке в RabbitMQ: {ex.Message}");
            }
        }
    }
}