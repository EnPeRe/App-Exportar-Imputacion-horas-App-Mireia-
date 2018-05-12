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

			imputacionesBl.ImportarImputaciones(@"C:\Users\daniel.graciaga\Downloads\Timesheet Report (6).csv");

			foreach (var imputacion in imputacionesBl.ListaImputaciones)
				Console.WriteLine(imputacion.ToString());
			Console.ReadLine();

			imputacionesBl.CalcularDiccionarioContractors(@"C:\Users\daniel.graciaga\Documents\Developers-Contractor.csv");
            imputacionesBl.CalcularContractors();

			foreach (var entrada in imputacionesBl.ContractorsDictionary)
				Console.WriteLine(entrada.Key + " : " + entrada.Value);

			imputacionesBl.CalcularBCs();

            foreach (var imputacion in imputacionesBl.ListaImputaciones)
                Console.WriteLine(imputacion.ToStringOut());
            Console.ReadLine();

            Console.WriteLine(imputacionesBl.Contador);
            Console.ReadLine();

        }
    }
}
