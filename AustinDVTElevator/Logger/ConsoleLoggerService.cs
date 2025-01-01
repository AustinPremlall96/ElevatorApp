using AustinDVTElevator.Business.Interface;

namespace AustinDVTElevator.Logger
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine($"[log]: {message}");
        }
    }
}
