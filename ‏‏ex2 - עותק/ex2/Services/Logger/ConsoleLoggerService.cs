namespace TasksApi.Services.Logger
{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log:{message}");
        }
    }
}
