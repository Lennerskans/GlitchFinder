using GlitchFinder.DataSources;
using GlitchFinder.Matrix;
using GlitchFinder.Matrix.Contracts;
using GlitchFinder.Reporters;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GlitchFinder.Manager
{
    public class Manager
    {
        private Action<string> writeOutput;


        public Manager(Action<string> writeOutput)
        {
            this.writeOutput = writeOutput;
        }

        public void NewComparison(string newSettingsFileName)
        {
            var comparisonSetting = new ComparisonSetting()
            {
                LeftDataSource = GetDefaultSettings(DataSourceType.MsSql),
                RightDataSource = GetDefaultSettings(DataSourceType.MsSql),
                ComparisonFields = new List<ComparisonField> { new ComparisonField() { LeftFieldName = "LeftMatrixField", RightFieldName = "RightMatrixField"  }  },
                ReportType = ReportType.Html,
                ReportFilePath = "#PATH_TO_FILE#"
            };

            JsonUtils.Save(newSettingsFileName, comparisonSetting);
        }

        public void NewRegressionTest(string newSettingsFileName)
        {
            var regressionTestSettings = new RegressionTestSetting()
            {
                BaselineFilePath = "#PATH_TO_FILE#",
                ReportFilePath = "#PATH_TO_FILE#",
                ReportType = ReportType.Csv,
                ComparisonFields = new List<string> { "Field1", "Field2" },
                SourceSetting = GetDefaultSettings(DataSourceType.MsSql)
            };

            JsonUtils.Save(newSettingsFileName, regressionTestSettings);
        }

        private SourceSettingContainer GetDefaultSettings(DataSourceType dataSourceType)
        {
            switch(dataSourceType)
            {
                case DataSourceType.CsvFile:
                    var csvFileSourceSetting = new CsvFileSourceSetting();
                    csvFileSourceSetting.SetExample();
                    return csvFileSourceSetting.ToSourceSettingContainer();
                case DataSourceType.MsSql:
                    var sqlSourceSetting = new SqlSourceSetting();
                    sqlSourceSetting.SetExample();
                    return sqlSourceSetting.ToSourceSettingContainer();
                default:
                    throw new NotImplementedException();
            }
        }

        public bool Compare(string settingsFile)
        {
            try
            {
                ComparisonSetting comparisonSetting = JsonUtils.Load<ComparisonSetting>(settingsFile);

                var isLeftOk = GetMatrix(comparisonSetting.LeftDataSource, out IMatrix leftMatrix);
                var isRightOk = GetMatrix(comparisonSetting.RightDataSource, out IMatrix rightMatrix);

                var reporter = GetReporter(comparisonSetting.ReportType);
                var reportFileName = comparisonSetting.ReportFilePath;

                if (isLeftOk && isRightOk)
                {
                    var isEqual = MatrixComparer.IsEqual(comparisonSetting.ComparisonFields, leftMatrix, rightMatrix, out IMatrix comparedMatrixes);

                    reporter.CreateReport(reportFileName, comparedMatrixes, isEqual);
                    if (!isEqual) 
                        writeOutput($"Differences written to {reportFileName}.html");
                    return isEqual;
                }

                reporter.NonUniqueKeys(reportFileName, leftMatrix, rightMatrix, false);

                return false;
            }
            catch (Exception e)
            {
                writeOutput($"Couldn't perform glitch detection\n\n{e.Message}");
                return false;
            }
        }

        public bool SetBaseline(string settingsFile)
        {
            try
            {
                RegressionTestSetting regressionTestSetting = JsonUtils.Load<RegressionTestSetting>(settingsFile);
                var isMatrixOk = GetMatrix(regressionTestSetting.SourceSetting, out IMatrix baselineMatrix);

                var serialized = ((Matrix.Matrix)baselineMatrix).Serialize();
                File.WriteAllText(regressionTestSetting.BaselineFilePath, serialized);
                return true;
            }
            catch(Exception e)
            {
                writeOutput($"Couldn't perform baselining\n\n{e.Message}");
                return false;
            }            
        }

        public bool RegressionTest(string settingsFile)
        {
            try
            {
                RegressionTestSetting regressionTestSetting = JsonUtils.Load<RegressionTestSetting>(settingsFile);

                var serializedData = File.ReadAllText(regressionTestSetting.BaselineFilePath);

                IMatrix baselineMatrix = new Matrix.Matrix(serializedData);
                var isMatrixOk = GetMatrix(regressionTestSetting.SourceSetting, out IMatrix newMatrix);

                var reporter = GetReporter(regressionTestSetting.ReportType);
                var reportFileName = regressionTestSetting.ReportFilePath;

                if (isMatrixOk)
                {
                    var comparisonFields = regressionTestSetting.ComparisonFields.Select(c => new ComparisonField { LeftFieldName = c, RightFieldName = c }).ToList();
                    var isEqual = MatrixComparer.IsEqual(comparisonFields, baselineMatrix, newMatrix, out IMatrix comparedMatrixes);

                    reporter.CreateReport(reportFileName, comparedMatrixes, isEqual);
                    if (!isEqual)
                        writeOutput($"Differences written to {reportFileName}.html");
                    return isEqual;
                }

                reporter.NonUniqueKeys(reportFileName, baselineMatrix, newMatrix, false);

                return false;
            }
            catch (Exception e)
            {
                writeOutput($"Couldn't perform regression test\n\n{e.Message}");
                return false;
            }
        }

        private static bool GetMatrix(SourceSettingContainer setting, out IMatrix matrix)
        {
            switch (setting.DataSourceType)
            {
                case DataSourceType.MsSql:
                    return new SqlImport().TryImport(setting, out matrix);                        
                case DataSourceType.CsvFile:
                    return new CsvFileImport().TryImport(setting, out matrix);
                default:
                    throw new NotImplementedException($"SourceType {setting.DataSourceType} is not implemented");
            }
        }

        private IGlitchReport GetReporter(ReportType reportType)
        {
            switch(reportType)
            {
                case ReportType.Html:
                    return new HtmlReport();
                case ReportType.Csv:
                    return new CsvReport();
                default:
                    throw new NotImplementedException($"ReportType {reportType} is not implemented");
            }
        }

    }
}
