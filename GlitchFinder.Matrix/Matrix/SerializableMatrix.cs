using GlitchFinder.Matrix.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchFinder.Matrix
{
    internal class SerializableMatrix
    {
        public List<Field> Fields { get; set; }
        public List<string> UniqueKeys { get; set; }
        public Dictionary<string, List<string>> Matrix { get; set; }

    }
}
