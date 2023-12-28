using Kafka.Domain.Entity.Common;

namespace Kafka.Domain.Entity
{
    public class Product : BaseEntity
    {
        public string Description { get; set; }
    }
}
