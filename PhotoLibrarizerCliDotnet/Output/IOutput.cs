namespace PhotoLibrarizerCliDotnet.Output
{
    // Define a delegate for the output
    public delegate void OutputDelegate(string message);

// Interface for the output
    public interface IOutput
    {
        void Print(string message);
    }

// Implementation using Console.WriteLine
    public class ConsoleOutput : IOutput
    {
        public void Print(string message)
    {
        Console.WriteLine(message);
    }
    }
}