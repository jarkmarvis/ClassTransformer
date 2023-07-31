using StandUpKitV2.Models;
using System.Text.Json;
using StandUpKitV2.Functions;
using System.Text;

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

        var stringBytes = Encoding.UTF8.GetBytes(classContent);
        transform.CodeContent = Convert.ToBase64String(stringBytes);


        string jsonFileName = Path.GetFileNameWithoutExtension(classFile) + ".json";
        jsonFileName = Path.Combine(transform.Path, jsonFileName);

        Console.WriteLine($"It's soggy, dumping to  {jsonFileName}.");

        var jsonFile = JsonSerializer.Serialize<CustomTransform>(transform);

        try
        {
            if (File.Exists(jsonFileName))
            {
                File.Delete(jsonFileName);
            }

            using (var writer = new StreamWriter(jsonFileName))
            {
                writer.Write(jsonFile);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"I can't delete the file {jsonFileName}.");
            Console.WriteLine(e.Message);
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

        try
        {
            var base64EncodedBytes = System.Convert.FromBase64String(transform.CodeContent);

            using (var writer = new StreamWriter(classFileName))
            {
                writer.Write(Encoding.UTF8.GetString(base64EncodedBytes));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"I can't decode the file {jsonFile}. Error: {e.Message}");
            return;
        }

        Console.WriteLine($"*Burp*, Now I'll dump it here {classFileName}.");
    }
}
