namespace HerrenHaus_API.Logging
{
    public class LoggingV2:ILogging
    {
        public void log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor= ConsoleColor.Red;
                Console.WriteLine("Error - " + message);
                Console.BackgroundColor= ConsoleColor.Black;
            }
            else
            {
                Console.WriteLine(message);
            }

        }
    }
}
