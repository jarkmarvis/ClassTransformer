using StandUpKitV2.Models;
using System.Text.Json;
using StandUpKitV2.Functions;

namespace StandUpKitV2;

/// <summary>
/// Make cereal with your favorite class file.
/// </summary>
public class ClassTransformer
{
    private CheckCabinet _validator;

    public ClassTransformer()
    {
        _validator = new CheckCabinet();
    }

    /// <summary>
    /// Cerealize a class file into a json file
    /// </summary>
    /// <param name="classFile">Class file to transform</param>
    public void Cerealize(string classFile)
    {
        if (!_validator.IsItEmpty(classFile))
        {
            return;
        }

        string classContent = File.ReadAllText(classFile);

        if (!_validator.IsItEmpty(classContent))
        {
            return;
        }

        Console.WriteLine($"Adding the malk.");

        CustomTransform transform = new CustomTransform();

        transform.Name = Path.GetFileName(classFile);

        transform.Path = Path.GetDirectoryName(classFile) ?? ".\\";

        transform.CodeContent = string.Format("<![CDATA[{0}]]>", classContent);

        string jsonFileName = Path.GetFileNameWithoutExtension(classFile) + ".json";
        jsonFileName = Path.Combine(transform.Path, jsonFileName);

        Console.WriteLine($"It's soggy, dumping to  {jsonFileName}.");

        var jsonFile = JsonSerializer.Serialize<CustomTransform>(transform);

        using (var writer = new StreamWriter(jsonFileName))
        {
            writer.Write(jsonFile);
        }
    }

    /// <summary>
    /// DeCerealize a json file into a class file
    /// </summary>
    /// <param name="jsonFile">Json file to transform</param>
    public void DeCerealize(string jsonFile)
    {
        if (!_validator.IsItEmpty(jsonFile))
        {
            return;
        }

        string jsonContent = File.ReadAllText(jsonFile);

        if (!_validator.IsItEmpty(jsonContent))
        {
            return;
        }

        CustomTransform transform = JsonSerializer.Deserialize<CustomTransform>(jsonContent);

        string classFileName = Path.Combine(transform.Path, transform.Name);

        using (var writer = new StreamWriter(classFileName))
        {
            writer.Write(transform.CodeContent.Replace("<![CDATA[", "").Replace("]]>", ""));
        }

        Console.WriteLine($"*Burp*, Now I'll dump it here {classFileName}.");
    }
}
