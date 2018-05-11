using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using System.Text;
using System.Net;

namespace ImputacionHoras.DataAccessCsv
{
    public class DaoCsv : IDaoCsv
    {
		string PathFileSalida;

        public DaoCsv()
        {
            this.PathFileSalida = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
									Resources.Resource.ImputacionesText);
        }

        public void ExportarExcelImputaciones(List<RowImputacion> listaImputaciones)
        {
            using (StreamWriter sw = File.AppendText(string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
									Resources.Resource.ImputacionesText)))
            {
                sw.WriteLine(Resources.Resource.Sep);
                foreach (var row in listaImputaciones)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }

		public List<DataDevelopers> ImportarExcelDataDevelopers(string pathFile)
		{
			List<DataDevelopers> dataDevelopers = new List<DataDevelopers>();

			//Create COM Objects. Create a COM object for everything that is referenced
			Application xlApp = new Application();
			Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
			_Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Range xlRange = xlWorksheet.UsedRange;

			//  excel is not zero based!!
			//  i starts in 2 to avoid that row 1, which has the headers
			for (int i = 2; i < xlRange.Rows.Count; i++)
			{
				DataDevelopers developers = new DataDevelopers();

				developers = ImportarEntradaDataDevelopers(xlRange.Rows[i]);
				dataDevelopers.Add(developers);
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

			return dataDevelopers;
		}
		private DataDevelopers ImportarEntradaDataDevelopers(Range row)
		{
			DataDevelopers dataDevelopers = new DataDevelopers();

			dataDevelopers.JiraUser = GetCelda(row, 1);
			dataDevelopers.Contractor = GetCelda(row, 3);
			return dataDevelopers;
		}

		public List<RowImputacion> ImportarExcelImputaciones(string pathFile)
        {
            List<RowImputacion> listaImputaciones = new List<RowImputacion>();

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
                RowImputacion imputacion = new RowImputacion();

                imputacion = ImportarEntradaImputacion(xlRange.Rows[i]);
                listaImputaciones.Add(imputacion);
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

            return listaImputaciones;
        }

        private RowImputacion ImportarEntradaImputacion(Range row)
        {
            RowImputacion imputacion = new RowImputacion();

            imputacion.Proyecto = GetCelda(row, 1);
            imputacion.Tipo = GetCelda(row, 2);
            imputacion.Key = GetCelda(row, 3);
            imputacion.Title = GetCelda(row, 4);
            imputacion.Creator = GetCelda(row, 5);
            imputacion.EpicName = GetCelda(row, 6);
            imputacion.RelatedProject = GetCelda(row, 7);
            imputacion.FechaImputacion = DateTime.FromOADate(Convert.ToDouble(GetCelda(row, 8)));
            imputacion.Usuario = GetCelda(row, 9);
            imputacion.HorasImputadas = float.Parse(GetCelda(row, 10));
            return imputacion;
        }

        private string GetCelda(Range row, int column)
        {
            if (row.Cells[1, column] != null && row.Cells[1, column].Value2 != null)
            { 
                string result = row.Cells[1, column].Value2.ToString();
                return result;
            }
            else
                return Resources.Resource.TextEmpty;
        }
		
	}
}
