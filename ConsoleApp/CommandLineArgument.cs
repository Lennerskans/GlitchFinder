using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GlitchFinder.Contracts;

namespace GlitchFinder.ConsoleApp
{
    class CommandLineArgument
    {
        public static bool TryParseCommandLineArgument(string[] args, out CommandLineArgument commandLineArgument)
        {
            if (Parse(args, out CommandLineArgument cmdl))
            {
                commandLineArgument = cmdl;
                return true;
            }

            Console.WriteLine("Usage:");
            Console.WriteLine("glfc <config file>");
            Console.WriteLine("Run comparison specified in config file\n");

            Console.WriteLine("glfc -NewComparison <config file>");
            Console.WriteLine("Create config file suitable for comparison\n\n");

            Console.WriteLine("glfc -Baseline <config file>");
            Console.WriteLine("Create a baseline before regression test\n");

            Console.WriteLine("glfc -RegressionTest <config file>");
            Console.WriteLine("Run regression test against baseline\n");

            Console.WriteLine("glfc -NewRegressionTest <config file>");
            Console.WriteLine("Create config file suitable for regression test\n\n");

            commandLineArgument = new CommandLineArgument();
            return false;
        }

        private static bool Parse(string[] args, out CommandLineArgument commandLineArgument)
        {
            commandLineArgument = new CommandLineArgument();

            if (args.Length == 0)
                return false;

            if (args[0].StartsWith("-"))
            {
                if (args[0] == "-Baseline" && args.Length == 2)
                {
                    commandLineArgument.Command = Command.Baseline;
                    commandLineArgument.Arguments = new List<string> { args[1] };
                    return true;
                }
                else if (args[0] == "-RegressionTest" && args.Length == 2)
                {
                    commandLineArgument.Command = Command.RegressionTest;
                    commandLineArgument.Arguments = new List<string> { args[1] };
                    return true;
                }
                else if (args[0] == "-NewComparison" && args.Length == 2)
                {
                    commandLineArgument.Command = Command.NewComparison;
                    commandLineArgument.Arguments = new List<string> { args[1] };
                    return true;
                }
                else if (args[0] == "-NewRegressionTest" && args.Length == 2)
                {
                    commandLineArgument.Command = Command.NewRegressionTest;
                    commandLineArgument.Arguments = new List<string> { args[1] };
                    return true;
                }

            }
            else if (args.Length == 1)
            {
                commandLineArgument.Command = Command.Compare;
                commandLineArgument.Arguments = args.ToList();

                return true;
            }

            return false;
        }

        public Command Command { get; set; }
        public List<string> Arguments { get; set; }
    }
}
