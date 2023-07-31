namespace StandUpKitV2.Functions;

public class CheckCabinet
{
    public CheckCabinet()
    {
        
    }

    public bool IsItEmpty(string classFile)
    {
        if (string.IsNullOrEmpty(classFile))
        {
            Console.WriteLine("No cereal was specified, can't find it if you don't tell me what you want.");
            return false;
        }

        return true;
    }
}
