using Kafka.Sdk;
using Kafka.Sdk.Interface;

namespace Kafka.Infrastructure.Producer
{
    public class ProducerProduct<TModel> : KafkaProduce<TModel>, IKafkaProduce<TModel>
    {
        protected override string BootstrapServers => "localhost:9092";
        protected override string SchemaRegistryUrl => "http://localhost:8081";
        protected override string Topic => "product-dev";
    }
}
