using System;


namespace GlitchFinder.ConsoleApp
{
    internal class Application
    {
        public Application()
        {

        }


        internal static bool Run(CommandLineArgument command)
        {
            bool isEqual;
            var mgr = new Manager.Manager(Output);
            switch (command.Command)
            {
                case Contracts.Command.Compare:
                    isEqual = mgr.Compare(command.Arguments[0]);
                    Output(isEqual
                        ? "No glitches found"
                        : "There are glitches in the matrix.");
                    return isEqual;
                case Contracts.Command.Baseline:
                    isEqual =  mgr.SetBaseline(command.Arguments[0]);
                    if (isEqual)
                        Output("Baseline set.");
                    return isEqual;
                case Contracts.Command.RegressionTest:
                    isEqual = mgr.RegressionTest(command.Arguments[0]);
                    Output(isEqual
                        ? "No glitches found"
                        : "There are glitches in the matrix.");
                    return isEqual;
                case Contracts.Command.NewComparison:
                    mgr.NewComparison(command.Arguments[0]);
                    return true;
                case Contracts.Command.NewRegressionTest:
                    mgr.NewRegressionTest(command.Arguments[0]);
                    return true;
                default:
                    throw new NotImplementedException();
            }
        }

        public static void Output(string output)
        {
            Console.WriteLine(output);
        }
    }
}