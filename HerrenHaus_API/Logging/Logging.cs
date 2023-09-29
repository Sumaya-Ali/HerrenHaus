namespace HerrenHaus_API.Logging
{
    public class Logging: ILogging
    {
        public void log(string message, string type)
        {
            if (type == "error")
            {
                Console.WriteLine("Error - " + message);
            }
            else
            {
                Console.WriteLine(message);
            }

        }
    }
}
