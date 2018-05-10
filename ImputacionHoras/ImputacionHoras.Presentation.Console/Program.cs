using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DataAccessCsv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.PresentationConsole
{
    class Program
    {
		static void Main(string[] args)
		{
			List<EntradaImputacion> listaImputaciones = new List<EntradaImputacion>();
            DaoCsv daoCsv = new DaoCsv();
            
            listaImputaciones = daoCsv.ImportarExcelImputaciones(@"C:\Users\diego.blazquez\Downloads\TimesheetReport.xls");
            
            Console.WriteLine("Proyecto\t Key\t Title\t EpicName\t RelatedProject\t Fecha\t Usuario\t Horas");
            foreach (var imputacion in listaImputaciones)
                Console.WriteLine(imputacion.ToString());
            Console.ReadLine();
        }
    }
}
