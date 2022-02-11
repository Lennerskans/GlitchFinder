using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchFinder.Contracts
{
    public enum Command
    {
        Compare,
        Baseline,
        RegressionTest,
        NewComparison,
        NewRegressionTest
    }
}
