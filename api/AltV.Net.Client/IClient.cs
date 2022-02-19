using AltV.Net.Client.CApi;

namespace AltV.Net.Client
{
    public interface IClient
    {
        public void LogInfo(string message);
        public void LogError(string message);
        void LogWarning(string message);
        void LogDebug(string message);
        ILibrary Library { get; }
    }
}