using System;
using System.IO;
using System.Xml.Serialization;

namespace StandUpKitV2.Migrations.Models;

class Program
{
    static void Main(string[] args)
    {   
        string action = args[0];
        string classFile = args[1];

        if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(classFile))
        {
            Console.WriteLine("Usage: Transformer 'action' 'file'.");
            Console.WriteLine("Actions: s = serialize and d = deserialize.");
            Console.WriteLine("Example: Transformer s MyClassFile.cs");
            return;
        }

        if (action.ToLower() == "s")
        {
            SerializeClass(classFile);
        }
        else if (action.ToLower() == "d")
        {
            DeserializeClass(classFile);
        }
        else
        {
            Console.WriteLine("Invalid action.");

        }

        Console.WriteLine("Done");
    }

    private static void SerializeClass(string classFile)
    {
        // Read the class file
        string classContent = File.ReadAllText(classFile);

        if (string.IsNullOrEmpty(classContent))
        {
            Console.WriteLine($"No data found in {classFile}");
            return;
        }

        // Create a new CustomClass instance
        CustomTransform customClass = new CustomTransform();

        // Create a new CustomFile instance
        CustomFile customFile = new CustomFile();

        // Set the path property to the class file name
        customFile.Path = classFile;

        // Escape the class file content with CDATA tags
        classContent = string.Format("<![CDATA[{0}]]>", classContent);

        // Set the CodeContent property to the class file content
        customFile.CodeContent = classContent;

        // Set the ClassFile property of the CustomClass instance
        customClass.ClassFile = customFile;

        string xmlFile = Path.GetFileNameWithoutExtension(classFile) + ".xml";

        // Serialize the CustomClass instance to XML
        var serializer = new XmlSerializer(typeof(CustomTransform));
        using (var writer = new StreamWriter(xmlFile))
        {
            serializer.Serialize(writer, customClass);
        }
    }

    private static void DeserializeClass(string classFile)
    {
        // Read XML data from file
        string xmlData = File.ReadAllText(classFile);

        if (string.IsNullOrEmpty(xmlData))
        {
            Console.WriteLine($"No data found in {classFile}");
            return;
        }

        // Deserialize the XML into CustomClass
        var serializer = new XmlSerializer(typeof(CustomTransform));
        using (var reader = new StringReader(xmlData))
        {
            CustomTransform customClass = (CustomTransform)serializer.Deserialize(reader);
            // Write the CData to the file specified in path property of the
            // File element removing the CDATA tags
            if (customClass.ClassFile != null)
            {
                File.WriteAllText(customClass.ClassFile.Path,
                    customClass.ClassFile.CodeContent.Replace("<![CDATA[", "").Replace("]]>", ""));
            }
        }
    }
}