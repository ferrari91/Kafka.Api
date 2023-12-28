using System.ComponentModel.DataAnnotations;

namespace Kafka.Domain.Entity.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; } 
    }
}
