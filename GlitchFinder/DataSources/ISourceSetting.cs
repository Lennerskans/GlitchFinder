using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GlitchFinder.Contracts;

namespace GlitchFinder.DataSources
{
    public interface ISourceSetting
    {
        DataSourceType DataSourceType { get; set; }
        IEnumerable<string> UniqueKeyFields { get; set; }

        void SetExample();
    }
}
