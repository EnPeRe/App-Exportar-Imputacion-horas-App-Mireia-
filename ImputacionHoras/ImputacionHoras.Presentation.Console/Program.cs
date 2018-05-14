using ImputacionHoras.Business.Logic;
using System;

namespace ImputacionHoras.PresentationConsole
{
    class Program
    {
		static void Main(string[] args)
		{
            ImputationBL imputationBusiness = new ImputationBL();

            // Obtenemos los datos de imputaciones
			imputationBusiness.ImportImputations(PresentationResources.TimesheetPathCsvDiego);
			foreach (var row in imputationBusiness.ImputationsList)
				Console.WriteLine(row.ToStringIn());
            Console.WriteLine("Press Enter");
			Console.ReadLine();

            // Calculamos contractors
            imputationBusiness.CalculateContractors(PresentationResources.ContractorsPathCsvDiego);

            foreach (var row in imputationBusiness.ContractorsDictionary)
                Console.WriteLine(row.Key + " : " + row.Value);
            Console.WriteLine("Press Enter");
            Console.ReadLine();
            foreach (var row in imputationBusiness.ImputationsList)
                Console.WriteLine(row.ToStringOut());
            Console.WriteLine("Press Enter");
            Console.ReadLine();

            // Calculamos billing concepts
            imputationBusiness.CalculateAllBillingConcepts("appPMO", "Vueling18");
            foreach (var row in imputationBusiness.ImputationsList)
                Console.WriteLine(row.ToStringOut());

            Console.WriteLine(imputationBusiness.Counter);
            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }
    }
}
