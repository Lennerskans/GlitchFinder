using System.Collections.Generic;

using GlitchFinder.Contracts;
using GlitchFinder.DataSources;

namespace GlitchFinder.DataSources
{
    public class SqlSourceSetting : ISourceSetting
    {
        public DataSourceType DataSourceType { get; set; }
        public IEnumerable<string> UniqueKeyFields { get; set; }
        public bool UseEncryption { get; set; }
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public string QueryFile { get; set; }

        public void SetExample()
        {
            DataSourceType = DataSourceType.MsSql;
            UniqueKeyFields = new List<string> { "PrimaryKeyField1", "PrimaryKeyField2" };
            UseEncryption = false;
            ConnectionString = "#CONNECTION STRING#";
            Query = "select * from table";
            QueryFile = null;
        }
    }
}