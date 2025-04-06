using LMS.Domain.Entity;
using LMS.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using Quartz;
using System.Text.Json;
namespace LMS.API;

public class QueriesToPsql(ILogger<QueriesToPsql> logger,
    IServiceProvider serviceProvider,
    IConfiguration configuration) : IJob
{
    private readonly ILogger<QueriesToPsql> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly string _connectionString = configuration["RabbitMQ:ConnectionString"];
    private readonly string _queueName = configuration["RabbitMQ:QueueName"] ?? "logs_queue";

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting RabbitMQ to PostgreSQL job at {time}", DateTime.Now);

        try
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Declare the queue (in case it doesn't exist yet)
            channel.QueueDeclare(
                queue: _queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            // Set prefetch count to process messages in batches
            channel.BasicQos(prefetchSize: 0, prefetchCount: 10, global: false);

            var messageCount = ProcessMessages(channel);

            _logger.LogInformation("Processed {count} messages from RabbitMQ", messageCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing messages from RabbitMQ");
        }

        return Task.CompletedTask;
    }

    private int ProcessMessages(IModel channel)
    {
        int processedCount = 0;
        bool hasMoreMessages = true;

        // Process messages in a loop until queue is empty
        while (hasMoreMessages)
        {
            // Try to get a message using BasicGet (non-blocking)
            var result = channel.BasicGet(_queueName, autoAck: false);

            if (result == null)
            {
                // No more messages in the queue
                hasMoreMessages = false;
            }
            else
            {
                try
                {
                    // Process the message
                    var message = Encoding.UTF8.GetString(result.Body.ToArray());
                    var examLog = JsonSerializer.Deserialize<ExamLogs>(message);

                    if (examLog != null)
                    {
                        // Save to PostgreSQL
                        using (var scope = _serviceProvider.CreateScope())
                        {
                            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

                            // If Id is set to 0, it will be auto-generated


                            dbContext.ExamLogs.Add(examLog);
                            dbContext.SaveChanges();

                            processedCount++;

                            // Acknowledge the message after successful processing
                            channel.BasicAck(result.DeliveryTag, multiple: false);
                        }
                    }
                    else
                    {
                        _logger.LogWarning("Failed to deserialize message: {message}", message);
                        // Acknowledge the invalid message to remove it from the queue
                        channel.BasicAck(result.DeliveryTag, multiple: false);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    // Reject the message and requeue it
                    channel.BasicNack(result.DeliveryTag, multiple: false, requeue: true);
                    // Stop processing to prevent potential infinite loop on persistent errors
                    hasMoreMessages = false;
                }
            }
        }

        return processedCount;
    }
}