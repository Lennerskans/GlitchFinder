using GlitchFinder.Matrix.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitchFinder.Matrix
{
    public class Cell : ICell
    {
        public Cell(IScalar value)
        {
            Value = value;
            CellAttribute = CellAttribute.None;
        }

        public IScalar Value { get; set; }
        public CellAttribute CellAttribute { get; set; }
    }
}
