namespace TasksApi.Services.Logger
{
    public class FileLoggerService : ILoggerService
    {
        private readonly string _filePath;

        public FileLoggerService(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_filePath, append: true))
                {
                    writer.WriteLine($"{DateTime.Now}:{message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"failed to log message: {ex.Message}");
            }
        }
    }
}
