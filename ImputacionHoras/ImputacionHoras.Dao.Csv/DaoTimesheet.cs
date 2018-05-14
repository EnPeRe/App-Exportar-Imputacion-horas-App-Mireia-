using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using ImputacionHoras.Common.Logic.Model;

namespace ImputacionHoras.DataAccess.Timesheet
{
    public class DaoTimesheet : IDaoTimesheet
    {
        #region Properties
        string PathFileOut;
        #endregion

        #region Constructors
        public DaoTimesheet()
        {
            this.PathFileOut = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
									Resources.TimesheetResources.OutputFileName);
        }
        #endregion

        #region Methods Csv RowImputation
        public List<RowImputation> ImportImputationsFromCsv(string pathFile)
        {
            var imputationsList = new List<RowImputation>();
            DeleteFirstAndLastLineCsv(pathFile);
            using (var reader = new StreamReader(pathFile))
            {
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    var values = row.Split(';');
                    imputationsList.Add(SetImputationValues(values));
                }
            }
            return imputationsList;
        }

        public void ExportImputationsToCsv(List<RowImputation> imputationsList)
        {
            using (StreamWriter sw = File.AppendText(string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                                    Resources.TimesheetResources.OutputFileName)))
            {
                sw.WriteLine(Resources.TimesheetResources.CsvHeader);
                foreach (var row in imputationsList)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }

        private RowImputation SetImputationValues(string[] values)
        {
            var imputation = new RowImputation();
            imputation.Project = values[0];
            imputation.Type = values[1];
            imputation.Key = values[2];
            imputation.Title = values[3];
            imputation.Creator = values[4];
            imputation.EpicName = values[5];
            imputation.RelatedProject = values[6];
            imputation.ImputationDate = DateTime.Parse(values[7]);
            imputation.PersonName = values[8];
            imputation.ImputedHours = float.Parse(values[9]);

            return imputation;
        }
        #endregion

        #region Methods Csv DataContractors
        public List<DataContractor> ImportDataContractorsFromCsv(string pathFile)
        {
            var dataContractorsList = new List<DataContractor>();
            DeleteFirstAndLastLineCsv(pathFile);
            using (var reader = new StreamReader(pathFile))
            {
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    var values = row.Split(';');
                    dataContractorsList.Add(SetDataContractorsValues(values));

                }
            }
            return dataContractorsList;
        }

        private DataContractor SetDataContractorsValues(string[] values)
        {
            DataContractor dataContractors = new DataContractor();
            dataContractors.JiraUser = values[0];
            dataContractors.Contractor = values[2];
            return dataContractors;
        }
        #endregion

        #region Methods Csv 
        public List<DataAssets> ImportAssetsFromCsv(string pathFile)
        {
            var dataContractorsList = new List<DataContractor>();
            DeleteFirstAndLastLineCsv(pathFile);
            using (var reader = new StreamReader(pathFile))
            {
                while (!reader.EndOfStream)
                {
                    var row = reader.ReadLine();
                    var values = row.Split(';');
                    dataContractorsList.Add(SetDataContractorsValues(values));

                }
            }
            return dataContractorsList;
        }

        private DataContractor SetAssetsValues(string[] values)
        {
            DataContractor dataContractors = new DataContractor();
            dataContractors.JiraUser = values[0];
            dataContractors.Contractor = values[2];
            return dataContractors;
        }
        #endregion

        #region Methods Commons Csv
        private void DeleteFirstAndLastLineCsv(string pathfile)
        {
            var lines = File.ReadAllLines(pathfile);
            File.WriteAllLines(pathfile, lines.Skip(1).Take(lines.Length - 2));
        }
        #endregion

        #region Methods Xls RowImputation
        public List<RowImputation> ImportImputationsFromXls(string pathFile)
        {
            var imputationsList = new List<RowImputation>();

            //Create COM Objects. Create a COM object for everything that is referenced
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;

            //  excel is not zero based!!
            //  i starts in 2 to avoid that row 1, which has the headers
            for (int i = 300; i < 400; i++)
            {
                var imputation = new RowImputation();

                imputation = ImportImputationRowXls(xlRange.Rows[i]);
                imputationsList.Add(imputation);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects: 
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return imputationsList;
        }

        private RowImputation ImportImputationRowXls(Range row)
        {
            var imputation = new RowImputation();

            imputation.Project = GetCell(row, 1);
            imputation.Type = GetCell(row, 2);
            imputation.Key = GetCell(row, 3);
            imputation.Title = GetCell(row, 4);
            imputation.Creator = GetCell(row, 5);
            imputation.EpicName = GetCell(row, 6);
            imputation.RelatedProject = GetCell(row, 7);
            imputation.ImputationDate = DateTime.FromOADate(Convert.ToDouble(GetCell(row, 8)));
            imputation.PersonName = GetCell(row, 9);
            imputation.ImputedHours = float.Parse(GetCell(row, 10));
            return imputation;
        }
        #endregion

        #region Methods Xls DataContractors
        public List<DataContractor> ImportDataContractorsFromXls(string pathFile)
        {
            var dataContractors = new List<DataContractor>();

            //Create COM Objects. Create a COM object for everything that is referenced
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;

            //  excel is not zero based!!
            //  i starts in 2 to avoid that row 1, which has the headers
            for (int i = 2; i < xlRange.Rows.Count; i++)
            {
                DataContractor contractor = new DataContractor();

                contractor = ImportDataContractorsRowXls(xlRange.Rows[i]);
                dataContractors.Add(contractor);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects: 
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return dataContractors;
        }
        
        private DataContractor ImportDataContractorsRowXls(Range row)
        {
            var dataContractor = new DataContractor();

            dataContractor.JiraUser = GetCell(row, 1);
            dataContractor.Contractor = GetCell(row, 3);
            return dataContractor;
        }
        #endregion

        #region Methods Commons Xls
        private string GetCell(Range row, int column)
        {
            if (row.Cells[1, column] != null && row.Cells[1, column].Value2 != null)
            { 
                string result = row.Cells[1, column].Value2.ToString();
                return result;
            }
            else
                return string.Empty;
        }
        #endregion
    }
}
