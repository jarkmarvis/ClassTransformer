namespace StandUpKitV2.Migrations.Models;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 2 || string.IsNullOrEmpty(args[0]) || string.IsNullOrEmpty(args[1]))
        {
            Console.WriteLine("Usage: Transformer 'action' 'file'.");
            Console.WriteLine("Actions: 'make' = serialize and 'eat' = deserialize.");
            Console.WriteLine("Example: Transformers MyClassFile.cs to MyClassFile.json");

            return;
        }

        string action = args[0];
        string classFile = args[1];

        ClassTransformer transformer = new ClassTransformer();

        if (action.ToLower() == "make")
        {
            transformer.Cerealize(classFile);
        }
        else if (action.ToLower() == "eat")
        {
            transformer.DeCerealize(classFile);
        }
        else
        {
            Console.WriteLine("Invalid action.");
        }

        Console.WriteLine("Done");
        return;
    }
}