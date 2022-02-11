using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GlitchFinder.DataSources;
using GlitchFinder.Reporters;

namespace GlitchFinder.Manager
{
//    public class RegressionTestSetting<TDataSource> where TDataSource : class, new()
    public class RegressionTestSetting
    {
        public string BaselineFilePath { get; set; }
        public SourceSettingContainer SourceSetting { get; set; }
        public IEnumerable<string> ComparisonFields { get; set; }
        public ReportType ReportType { get; set; }
        public string ReportFilePath { get; set; }
    }
}
