using ImputacionHoras.Business.Logic;
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
            ImputacionBL imputacionesBl = new ImputacionBL();
            
            imputacionesBl.ImportarImputaciones(@"C:\Users\diego.blazquez\Downloads\TimesheetReport.xls");

            Console.WriteLine("Proyecto\t Key\t Title\t EpicName\t RelatedProject\t Fecha\t Usuario\t Horas");
            foreach (var imputacion in imputacionesBl.ListaImputacionesIn)
                Console.WriteLine(imputacion.ToString());
            Console.ReadLine();

            imputacionesBl.CalcularSalidas();

            foreach (var imputacion in imputacionesBl.ListaImputacionesOut)
                Console.WriteLine(imputacion.ToString());
            Console.ReadLine();

            Console.WriteLine(imputacionesBl.contador);
            Console.ReadLine();
        }
    }
}
