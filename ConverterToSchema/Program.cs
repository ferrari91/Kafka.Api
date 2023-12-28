using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Kafka.Domain.Entity;
using Kafka.Domain.Record;
using System.Diagnostics;


namespace ConverterToSchema
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao = 0;

            do
            {
                MensagemAbertura();
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Create Schema");
                Console.WriteLine("2 - Sair");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.Clear();
                    Console.WriteLine("Posição inválida. Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Executando comando...");
                        string name = "ProductRecord";
                        CreateAvroSchema<ProductRecord>(name);
                        string command = @$"avrogen -s C:\Schemas\{name}.avsc C:\Schemas --skip-directories";
                        ExecuteCommand(command);
                        break;
                    case 2:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            } while (opcao != 2);
        }

        static void MensagemAbertura()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("***********************************************");
            Console.WriteLine("*                                             *");
            Console.WriteLine("*                BEM-VINDO AO GERADOR DE      *");
            Console.WriteLine("*                SCHEMA PARA O KAFKA.         *");
            Console.WriteLine("*                                             *");
            Console.WriteLine("***********************************************");
            Console.ResetColor();
        }

        static void ExecuteCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "cmd.exe";
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            process.StandardInput.WriteLine(command);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();
        }

        static void CreateAvroSchema<T>(string name)
        {
            var builder = new Chr.Avro.Abstract.SchemaBuilder();
            var schema = builder.BuildSchema(typeof(T));

            var writer = new Chr.Avro.Representation.JsonSchemaWriter();
            var localSchema = @"C:\Schemas";

            if (!Directory.Exists(localSchema))
                Directory.CreateDirectory(localSchema);

            localSchema = Path.Combine(localSchema, name + ".avsc");

            using (var stream = new FileStream(localSchema, FileMode.OpenOrCreate))
                writer.Write(schema, stream);
        }
    }
}