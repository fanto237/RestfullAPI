namespace MagicVillaAPI.Logging;

public class Logging : ILogging
{
    public void LogInformation(string message, int type)
    {
        using (var writer = new StreamWriter("log/villa.txt"))
        {
            writer.WriteLine(message);
        }
    }

    public void LogError(string message, int type)
    {
        using (var writer = new StreamWriter("log/villa.txt"))
        {
            writer.WriteLine("ERROR - " + message);
        }
    }
}