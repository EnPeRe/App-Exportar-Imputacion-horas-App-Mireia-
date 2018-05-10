using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;

namespace ImputacionHoras.DataAccessCsv
{
    public class DaoCsv : IDaoCsv
    {
        string PathFileSalida;

        public DaoCsv()
        {
            this.PathFileSalida = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                                    @"/imputaciones.csv");
        }

        public void ExportarExcelImputaciones(List<EntradaImputacion> listaImputaciones)
        {
            using (StreamWriter sw = File.AppendText(string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
									@"/imputaciones.csv")))
            {
                sw.WriteLine("sep=,");
                foreach (var row in listaImputaciones)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }

        public List<EntradaImputacion> ImportarExcelImputaciones(string pathFile)
        {
            List<EntradaImputacion> listaImputaciones = new List<EntradaImputacion>();

            //Create COM Objects. Create a COM object for everything that is referenced
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            
            //  excel is not zero based!!
            //  i starts in 2 to avoid that row 1, which has the headers
            for (int i = 2; i <= 10; i++)
            {
                EntradaImputacion imputacion = new EntradaImputacion();

                imputacion = ImportarEntradaImputacion(xlRange.Cells[i]);
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

        private EntradaImputacion ImportarEntradaImputacion(Range row)
        {
            EntradaImputacion imputacion = new EntradaImputacion();

            imputacion.Proyecto = GetCelda(row, 1);
            imputacion.Tipo = GetCelda(row, 2);
            imputacion.Key = GetCelda(row, 3);
            imputacion.Title = GetCelda(row, 4);
            imputacion.EpicName = GetCelda(row, 5);
            //imputacion.FechaImputacion = Convert.ToDateTime(GetCelda(row, 6));
            imputacion.FechaImputacion = DateTime.Now;
            imputacion.Usuario = GetCelda(row, 7);
            imputacion.HorasImputadas = float.Parse(GetCelda(row, 8));
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
                return "";
        }
    }
}
