using Avro;
using Avro.IO;
using Avro.Specific;

namespace SchemaCreater
{
    public class ConverterClass
    {
        public static byte[] ConvertToAvro<T>(T classObj)
        {
            using (var stream = new MemoryStream())
            {
                var writer = new SpecificDefaultWriter(Schema.Parse(File.ReadAllText($"{nameof(classObj)}.avsc")));
                var encoder = new BinaryEncoder(stream);
                writer.Write(classObj, encoder);
                encoder.Flush();
                return stream.ToArray();
            }
        }
    }
}
