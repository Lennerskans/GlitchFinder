using System;

namespace GlitchFinder.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (CommandLineArgument.TryParseCommandLineArgument(args, out var command))
                Application.Run(command);
        }
    }
}
