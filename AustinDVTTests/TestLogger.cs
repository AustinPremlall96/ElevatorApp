using AustinDVTElevator.Business.Interface;

namespace AustinDVTTests
{
    public class TestLogger : ILoggerService
    {
        public List<string> Messages { get; } = new List<string>();

        public void Log(string message)
        {
            Messages.Add(message);
        }
    }
}
