using Kafka.Domain.Record;
using Kafka.Sdk.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaController : ControllerBase
    {
        protected readonly IKafkaProduce<ProductRecord> _producer;

        public KafkaController(IKafkaProduce<ProductRecord> producer)
        {
            _producer = producer;
        }

        [HttpPost]
        public async Task Register([FromBody] string productDescription)
        {
            try
            {
               await _producer.Produce(new ProductRecord() { Id = 1, Description = productDescription });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
