using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImputacionHoras.DataAccessJira
{
    public class DaoJira : IDaoJira
	{
		public DaoJira()
		{
		}

		public List<RowImputacion> GetData(string usuario, string contraseña, DateTime FromDate, DateTime ToDate)
		{
			throw new NotImplementedException();
		}

		public static void getExcelFile(string pathFile)
		{

			//Create COM Objects. Create a COM object for everything that is referenced
			Excel.Application xlApp = new Excel.Application();
			Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(pathFile);
			Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
			Excel.Range xlRange = xlWorksheet.UsedRange;

			int rowCount = xlRange.Rows.Count;
			int colCount = xlRange.Columns.Count;

			//iterate over the rows and columns and print to the console as it appears in the file
			//excel is not zero based!!
			for (int i = 1; i <= rowCount; i++)
			{
				for (int j = 1; j <= colCount; j++)
				{
					//new line
					if (j == 1)
						Console.Write("\r\n");

					//write the value to the console
					if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
						Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");
				}
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
		}
		public Dictionary<string, string> GetDictionary()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();

			dictionary.Add("Ancillaries", "Ancillaries");
			dictionary.Add("TBL", "Tablets");
			dictionary.Add("AMS", "AMS");
			dictionary.Add("VVW", "Venta Vuelo Web ");
			dictionary.Add("I3", "I3");
			dictionary.Add("RMT", "REMO");
			dictionary.Add("RMT2", "REMO");
			dictionary.Add("LOYAL", "Loyalty");
			dictionary.Add("MICE", "Sales");
			dictionary.Add("VC", "vy Content");
			dictionary.Add("SP", "Service Product");
			return dictionary;
		}
	}
}
