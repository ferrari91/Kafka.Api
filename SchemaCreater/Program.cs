using Microsoft.CSharp;
using SchemaCreater;
using System.CodeDom.Compiler;
using System.Reflection;

int option = 0;

while (option != 2)
{
    Console.WriteLine("Menu:");
    Console.WriteLine("1. Selecionar arquivo");
    Console.WriteLine("2. Sair");
    Console.Write("Escolha uma opção: ");

    if (int.TryParse(Console.ReadLine(), out option))
    {
        switch (option)
        {
            case 1:
                SelectFile();
                break;
            case 2:
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
}

static void SelectFile()
{
    Console.WriteLine("Selecione um arquivo com extensão .cs");
    string filePath = Console.ReadLine();

    Console.WriteLine("Informe o nome da Classe.");
    string className = Console.ReadLine();

    object classObj = null;

    while (classObj == null)
    {
        if (Path.GetExtension(filePath) == ".cs")
        {
            var fileContent = File.ReadAllText(filePath);
            var assembly = CompileCode(fileContent);

            var classType = assembly.GetType(className);
            classObj = Activator.CreateInstance(classType);

            ConverterClass.ConvertToAvro(classObj);
        }
        else
        {
            Console.WriteLine("Extensão inválida!");
        }
    }

}

static Assembly CompileCode(string code)
{
    var provider = new CSharpCodeProvider();
    var parameters = new CompilerParameters();

    parameters.GenerateExecutable = false;
    parameters.GenerateInMemory = true;

    return provider.CompileAssemblyFromSource(parameters, code).CompiledAssembly;
}
