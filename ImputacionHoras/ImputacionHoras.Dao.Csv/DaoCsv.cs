﻿using ImputacionHoras.Common.Logic.Modelo;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ImputacionHoras.DaoCsv
{
    public class DaoCsv : IDaoCsv
    {
        string PathFile;

        public DaoCsv()
        {
            this.PathFile = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                                    @"/imputaciones.csv");
        }

        public void ExportarImputaciones(List<EntradaImputacion> listaImputaciones)
        {
            using (StreamWriter sw = File.AppendText(PathFile))
            {
                sw.WriteLine("sep=,");
                foreach (var row in listaImputaciones)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }

        public List<EntradaImputacion> ImportarImputaciones()
        {
            ApplicationClass app = new ApplicationClass();
            Workbook book = null;
            Range range = null;

            try
            {
                app.Visible = false;
                app.ScreenUpdating = false;
                app.DisplayAlerts = false;

                string execPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                book = app.Workbooks.Open(@"C:\data.xls", Missing.Value, Missing.Value, Missing.Value
                                                  , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                 , Missing.Value, Missing.Value, Missing.Value, Missing.Value
                                                , Missing.Value, Missing.Value, Missing.Value);
                foreach (Worksheet sheet in book.Worksheets)
                {

                    //Console.WriteLine(@"Values for Sheet " + sheet.Index);

                    // get a range to work with
                    range = sheet.get_Range("A1", Missing.Value);
                    // get the end of values to the right (will stop at the first empty cell)
                    range = range.get_End(XlDirection.xlToRight);
                    // get the end of values toward the bottom, looking in the last column (will stop at first empty cell)
                    range = range.get_End(XlDirection.xlDown);

                    // get the address of the bottom, right cell
                    string downAddress = range.get_Address(
                        false, false, XlReferenceStyle.xlA1,
                        Type.Missing, Type.Missing);

                    // Get the range, then values from a1
                    range = sheet.get_Range("A1", downAddress);
                    object[,] values = (object[,])range.Value2;

                    // View the values
                    Console.Write("\t");
                    Console.WriteLine();
                    for (int i = 1; i <= values.GetLength(0); i++)
                    {
                        for (int j = 1; j <= values.GetLength(1); j++)
                        {
                            Console.Write("{0}\t", values[i, j]);
                        }
                        Console.WriteLine();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                range = null;
                if (book != null)
                    book.Close(false, Missing.Value, Missing.Value);
                book = null;
                if (app != null)
                    app.Quit();
                app = null;
            }
        }


    }
}
