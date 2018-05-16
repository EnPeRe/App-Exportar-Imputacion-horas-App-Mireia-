using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using ImputacionHoras.Common.Logic.Model;
using System.Text;
using ImputacionHoras.DataAccess.Timesheet.Resources;
using ImputacionHoras.Common.Logic.CustomExceptions;

namespace ImputacionHoras.DataAccess.Timesheet
{
    public class DaoTimesheet : IDaoTimesheet
    {
        #region Properties
        private string PathFileOut;
        #endregion

        #region Constructors
        public DaoTimesheet()
        {
            this.PathFileOut = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
									TimesheetResources.OutputFileName);
        }
        #endregion

        #region Methods Csv RowImputation
        public List<RowImputation> ImportImputationsFromCsv(string pathFile)
        {
            var imputationsList = new List<RowImputation>();

            try
            {
                var lengthReader = File.ReadAllLines(pathFile).Length;
                var countLine = 0;
                using (var reader = new StreamReader(pathFile, Encoding.GetEncoding(TimesheetResources.ISO8859Encoding)))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        countLine++;
                        if (countLine == lengthReader - 1)
                        {
                            reader.ReadLine();
                        }
                        else
                        {
                            var row = reader.ReadLine();
                            var values = row.Split(';');
                            imputationsList.Add(SetImputationValues(values));
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }

            return imputationsList;
        }

        public void ExportImputationsToCsv(string pathToExport, List<RowImputation> imputationsList)
        {
            try
            {
                var dateTimeString = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
                var filePath = string.Concat(pathToExport,
                                            TimesheetResources.OutputFileName,
                                            dateTimeString,
                                            TimesheetResources.Format);

                using (var sw = new StreamWriter(filePath, false, Encoding.GetEncoding(TimesheetResources.ISO8859Encoding)))
                {
                    sw.WriteLine(TimesheetResources.CsvHeader);
                    foreach (var row in imputationsList)
                    {
                        sw.WriteLine(row.ToStringCsv());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }
        }

        private RowImputation SetImputationValues(string[] values)
        {
            var imputation = new RowImputation();

            try
            {
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
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }

            return imputation;
        }
        #endregion

        #region Methods Csv DataContractors
        public List<DataContractor> ImportDataContractorsFromCsv(string pathFile)
        {
            var dataContractorsList = new List<DataContractor>();
            try
            {
                using (var reader = new StreamReader(pathFile))
                {
                    // Ignoramos la primera linea porque es el header del fichero
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var row = reader.ReadLine();
                        var values = row.Split(';');
                        dataContractorsList.Add(SetDataContractorsValues(values));

                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }
            return dataContractorsList;
        }

        private DataContractor SetDataContractorsValues(string[] values)
        {
            DataContractor dataContractor = new DataContractor();

            try
            {
                dataContractor.JiraUser = values[0];
                dataContractor.Contractor = values[2];

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }

            return dataContractor;
        }
        #endregion

        #region Methods Csv DataAssets
        public List<DataAsset> ImportAssetsFromCsv(string pathFile)
        {
            var dataAssetsList = new List<DataAsset>();
            try
            {
                using (var reader = new StreamReader(pathFile))
                {
                    while (!reader.EndOfStream)
                    {
                        var row = reader.ReadLine();
                        var values = row.Split(';');
                        dataAssetsList.Add(SetAssetsValues(values));

                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }
            return dataAssetsList;
        }

        private DataAsset SetAssetsValues(string[] values)
        {
            DataAsset dataAsset = new DataAsset();

            try
            {
                dataAsset.Product = values[0];
                dataAsset.Asset = values[1];

            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }
            return dataAsset;
        }
        #endregion

        // Los metodos basados en Xls estan deprecated, ya que son muy ineficientes
        #region Methods Xls RowImputation
        //public List<RowImputation> ImportImputationsFromXls(string pathFile)
        //{
        //    var imputationsList = new List<RowImputation>();

        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Application xlApp = new Application();
        //    Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
        //    _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Range xlRange = xlWorksheet.UsedRange;

        //    int rowCount = xlRange.Rows.Count;

        //    //  excel is not zero based!!
        //    //  i starts in 2 to avoid that row 1, which has the headers
        //    for (int i = 2; i < rowCount; i++)
        //    {
        //        var imputation = ImportImputationRowXls(xlRange.Rows[i]);
        //        imputationsList.Add(imputation);
        //    }

        //    //cleanup
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    //rule of thumb for releasing com objects: 
        //    //  never use two dots, all COM objects must be referenced and released individually
        //    //  ex: [somthing].[something].[something] is bad

        //    //release com objects to fully kill excel process from running in the background
        //    Marshal.ReleaseComObject(xlRange);
        //    Marshal.ReleaseComObject(xlWorksheet);

        //    //close and release
        //    xlWorkbook.Close();
        //    Marshal.ReleaseComObject(xlWorkbook);

        //    //quit and release
        //    xlApp.Quit();
        //    Marshal.ReleaseComObject(xlApp);

        //    return imputationsList;
        //}

        //private RowImputation ImportImputationRowXls(Range row)
        //{
        //    var imputation = new RowImputation
        //    {
        //        Project = GetCell(row, 1),
        //        Type = GetCell(row, 2),
        //        Key = GetCell(row, 3),
        //        Title = GetCell(row, 4),
        //        Creator = GetCell(row, 5),
        //        EpicName = GetCell(row, 6),
        //        RelatedProject = GetCell(row, 7),
        //        ImputationDate = DateTime.FromOADate(Convert.ToDouble(GetCell(row, 8))),
        //        PersonName = GetCell(row, 9),
        //        ImputedHours = float.Parse(GetCell(row, 10))
        //    };
        //    return imputation;
        //}
        #endregion

        #region Methods Xls DataContractors
        //public List<DataContractor> ImportDataContractorsFromXls(string pathFile)
        //{
        //    var dataContractors = new List<DataContractor>();

        //    //Create COM Objects. Create a COM object for everything that is referenced
        //    Application xlApp = new Application();
        //    Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
        //    _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
        //    Range xlRange = xlWorksheet.UsedRange;

        //    //  excel is not zero based!!
        //    //  i starts in 2 to avoid that row 1, which has the headers
        //    for (int i = 2; i < xlRange.Rows.Count; i++)
        //    {
        //        DataContractor contractor = ImportDataContractorsRowXls(xlRange.Rows[i]);
        //        dataContractors.Add(contractor);
        //    }

        //    //cleanup
        //    GC.Collect();
        //    GC.WaitForPendingFinalizers();

        //    //rule of thumb for releasing com objects: 
        //    //  never use two dots, all COM objects must be referenced and released individually
        //    //  ex: [somthing].[something].[something] is bad

        //    //release com objects to fully kill excel process from running in the background
        //    Marshal.ReleaseComObject(xlRange);
        //    Marshal.ReleaseComObject(xlWorksheet);

        //    //close and release
        //    xlWorkbook.Close();
        //    Marshal.ReleaseComObject(xlWorkbook);

        //    //quit and release
        //    xlApp.Quit();
        //    Marshal.ReleaseComObject(xlApp);

        //    return dataContractors;
        //}
        
        //private DataContractor ImportDataContractorsRowXls(Range row)
        //{
        //    var dataContractor = new DataContractor
        //    {
        //        JiraUser = GetCell(row, 1),
        //        Contractor = GetCell(row, 3)
        //    };
        //    return dataContractor;
        //}
        #endregion

        #region Methods Commons Xls
        //private string GetCell(Range row, int column)
        //{
        //    if (row.Cells[1, column] != null && row.Cells[1, column].Value2 != null)
        //    { 
        //        string result = row.Cells[1, column].Value2.ToString();
        //        return result;
        //    }
        //    else
        //        return string.Empty;
        //}
        #endregion
    }
}
