
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using GlitchFinder.Matrix;
using GlitchFinder.Matrix.Contracts;

namespace GlitchFinder.MatrixTest
{
    public class Tests
    {
        List<List<string>> rawMatrix1, rawMatrix2, identicalButOther;
        List<string> fieldNames;

        [SetUp]
        public void Setup()
        {
            // Mock matrixes
            rawMatrix1 = BuildRawMatrix(true, out fieldNames);
            identicalButOther = BuildRawMatrix(true, out fieldNames);
            rawMatrix2 = BuildRawMatrix(false, out var throwAway);
        }

        [Test]
        public void TestParseMatrix()
        {
            MatrixCreator.ParseMatrix(fieldNames, new List<string> { fieldNames[0] }, rawMatrix1, out var matrix);
            Assert.Pass(); 
        }

        [Test]
        public void TestCompareIdentical()
        {
            MatrixCreator.ParseMatrix(fieldNames, new List<string> { fieldNames[0] }, rawMatrix1, out var leftMatrix);
            MatrixCreator.ParseMatrix(fieldNames, new List<string> { fieldNames[0] }, identicalButOther, out var rightMatrix);

            var compareFields = fieldNames.Where(f => f != "Key").Select(f => new ComparisonField { LeftFieldName = f, RightFieldName = f }).ToList();

            var isEqual = MatrixComparer.IsEqual(compareFields, leftMatrix, rightMatrix, out var differences);
            Assert.AreEqual(isEqual, true);
        }

        [Test]
        public void TestCompareDifferent()
        {
            MatrixCreator.ParseMatrix(fieldNames, new List<string> { fieldNames[0] }, rawMatrix1, out var leftMatrix);
            MatrixCreator.ParseMatrix(fieldNames, new List<string> { fieldNames[0] }, rawMatrix2, out var rightMatrix);

            var compareFields = fieldNames.Where(f => f != "Key").Select(f => new ComparisonField { LeftFieldName = f, RightFieldName = f }).ToList();

            var isEqual = MatrixComparer.IsEqual(compareFields, leftMatrix, rightMatrix, out var differences);
            Assert.AreNotEqual(isEqual, true);
        }


        private List<List<string>> BuildRawMatrix(bool alternative, out List<string> fieldNames)
        {
            Random random = new Random(42);

            fieldNames = new List<string>() { "Key" };

            var data = new List<List<string>>();
            for (int r = 0; r < 12; r++)
            {
                var row = new List<string>();
                row.Add(r.ToString());
                for (int c = 0; c < 12; c++)
                {
                    if (r == 0)
                        fieldNames.Add("Column" + c);

                    var number = random.NextDouble();
                    if (r == 5 && c == 5 && alternative)
                        number = 42;

                    row.Add(((decimal)number).ToString());
                }
                if (!(r == 7 && alternative) && !(r == 8 && !alternative))
                    data.Add(row);
            }

            return data;
        }
    }
}