﻿
using GlitchFinder.Matrix.Contracts;

namespace GlitchFinder.Reporters
{
    public interface IGlitchReport
    {
        void CreateReport(string filePath, IMatrix comparison, bool isEqual);
        void NonUniqueKeys(string filePath, IMatrix leftMatrix, IMatrix rightMatrix, bool isEqual);
    }
}
