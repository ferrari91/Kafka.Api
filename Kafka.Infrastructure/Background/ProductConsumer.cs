using Kafka.Domain.Record;
using Kafka.Sdk;
using Newtonsoft.Json;

namespace Kafka.Infrastructure.Background
{
    public class ProductConsumer : KafkaConsumeService<ProductConsumer, ProductRecord>
    {
        public ProductConsumer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string BootstrapServers => "localhost:9092";
        protected override string SchemaRegistryUrl => "http://localhost:8081";
        protected override string Topic => "product-dev";
        protected override string GroupId => "product-consumer-group";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine(ConsumeResult?.Value.Description);

            await Task.CompletedTask;
        }

        public override async Task OnExceptionAsync(Exception ex)
        {
            Console.WriteLine($"{ex.Message}");

            await Task.CompletedTask;
        }
    }
}
