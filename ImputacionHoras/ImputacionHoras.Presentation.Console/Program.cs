using ImputacionHoras.Business.Logic;
using ImputacionHoras.Common.Logic.Model;
using System;
using System.Collections.Generic;

namespace ImputacionHoras.PresentationConsole
{
    class Program
    {
		static void Main(string[] args)
		{
            ImputationBL imputationBusiness = new ImputationBL();
            string usuario = string.Empty;
            string contraseña = string.Empty;

            Console.WriteLine("Introduzca usuario de Jira");
            usuario = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Introduzca contraseña de Jira");
            contraseña = Console.ReadLine();
            Console.Clear();

            // Obtenemos los datos de imputaciones
            Console.WriteLine("Importando imputaciones");
			imputationBusiness.ImportImputations(PresentationResources.TimesheetPathCsvDiego);

            // Calculamos contractors
            Console.WriteLine("Calculando Contractors");
            imputationBusiness.CalculateContractors(PresentationResources.ContractorsPathCsvDiego);

            // Calculamos billing concepts
            Console.WriteLine("Calculando Billing Concepts");
            imputationBusiness.CalculateAllBillingConcepts(usuario, contraseña); // (usuarioJira, contraseñaJira)

            // Calculamos assets
            Console.WriteLine("Calculando Assets");
            imputationBusiness.CalculateAssets(PresentationResources.AssetsPathCsvDiego);

            // Exportamos a CSV
            Console.WriteLine("Exportando a Csv");
            imputationBusiness.ExportImputations();

            Console.WriteLine("Llamadas a la API realizadas: " + imputationBusiness.Counter);
            Console.WriteLine("Presiona Enter para terminar");
            Console.ReadLine();
        }
    }
}
