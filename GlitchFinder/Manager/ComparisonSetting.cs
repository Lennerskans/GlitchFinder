using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using GlitchFinder.DataSources;
using GlitchFinder.Matrix;
using GlitchFinder.Matrix.Contracts;
using GlitchFinder.Reporters;

namespace GlitchFinder.Manager
{
//    public class ComparisonSetting<TLeftDataSource, TRightDataSource> where TLeftDataSource : class, new() where TRightDataSource : class, new()
    public class ComparisonSetting
    {
        public SourceSettingContainer LeftDataSource { get; set; }
        public SourceSettingContainer RightDataSource { get; set; }
        public List<ComparisonField> ComparisonFields { get; set; }
        public ReportType ReportType { get; set; }
        public string ReportFilePath { get; set; }
    }
}
