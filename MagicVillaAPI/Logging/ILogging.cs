namespace MagicVillaAPI.Logging;

public interface ILogging
{
    void LogInformation(string message, int type = 0);
    void LogError(string message, int type = 0);
}