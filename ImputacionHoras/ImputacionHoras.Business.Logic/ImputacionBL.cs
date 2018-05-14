using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DataAccessCsv;
using ImputacionHoras.DataAccessJira;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Business.Logic
{
    public class ImputacionBL
    {
        private readonly DaoCsv DataAccessCsv;
        private readonly DaoJira DataAccessJira;
        public List<RowImputation> ListaImputaciones { get; set; }
        public Dictionary<string, string> BillingConceptDictionary { get; set; }
        public Dictionary<string, string> ContractorsDictionary { get; set; }
        public int Contador = 0;

        // Constructor vacio
        public ImputacionBL()
        {
            DataAccessCsv = new DaoCsv();
            DataAccessJira = new DaoJira();
            ListaImputaciones = new List<RowImputation>();
            BillingConceptDictionary = new Dictionary<string, string>();
            ContractorsDictionary = new Dictionary<string, string>();
        }

        // Constructor completo
        public ImputacionBL(DaoCsv dataAccessCsv, DaoJira dataAccessJira, List<RowImputation> listaImputacionesIn, Dictionary<string, string> billingConceptDictionary, Dictionary<string, string> contractorsDictionary)
        {
            DataAccessCsv = dataAccessCsv;
			DataAccessJira = dataAccessJira;
            ListaImputaciones = listaImputacionesIn;
            BillingConceptDictionary = billingConceptDictionary;
            ContractorsDictionary = contractorsDictionary;
        }

        // Lee el fichero .csv con trabajadores-empresas y calcula la empresa de cada Row
        public void CalcularDiccionarioContractors(string pathFile)
        {
            List<DataContractor> dataDevelopers = new List<DataContractor>();
            dataDevelopers = DataAccessCsv.ImportarCsvDataDevelopers(pathFile);
            foreach (var entrada in dataDevelopers)
                ContractorsDictionary.Add(entrada.JiraUser, entrada.Contractor);
        }

        public void ImportarImputaciones(string pathFile)
        {
            this.ListaImputaciones = DataAccessCsv.ImportarCsvImputaciones(pathFile);
        }

		

        public void CalcularContractors()
        {


            foreach(var entrada in ListaImputaciones)
            {
                if (ContractorsDictionary.ContainsKey(entrada.Creator))
                    entrada.Contractor = ContractorsDictionary[entrada.Creator];
            }
        }
        
		public void CalcularAllBillingConcepts()
        {
            foreach (var imputacion in ListaImputaciones)
            {
                imputacion.BillingConcept = CalcularBillingConcept(imputacion);
            }
        }

        public string CalcularBillingConcept(RowImputation salidaImputacion)
        {
            var billingConcept = string.Empty;

            if (BillingConceptDictionary.ContainsKey(salidaImputacion.Key))
            {
                billingConcept = BillingConceptDictionary[salidaImputacion.Key];
            }
            else
            {
                if ((salidaImputacion.RelatedProject != "") && (salidaImputacion.RelatedProject != Resources.Resource.EmptyText))
                {
                    billingConcept = salidaImputacion.RelatedProject;
                }
                else if (salidaImputacion.EpicName != "")
                {
                    billingConcept = salidaImputacion.EpicName;
                }
                else

				{
					string parentKey = SplitParentKey(salidaImputacion.Title);
					if (salidaImputacion.Title != parentKey && parentKey.Length < 15)
					{
						RowImputation rowImputacionParentKey = DataAccessJira.GetDataFromParentKey(parentKey, "", "");
						billingConcept = CalcularBillingConcept(rowImputacionParentKey);
						rowImputacionParentKey.Key = salidaImputacion.Key;
					}
					else
					{
						billingConcept = salidaImputacion.Title;
					}
                    // Sacar posible ParentKey
                    // Si ParentKey sí existe -> Peticion Api de datos de ParentKey
                    // BC = CalcularBillingConcept(Parent)

                    // Si ParentKey no existe
                   
                }

                BillingConceptDictionary.Add(salidaImputacion.Key, billingConcept);
            }

            return billingConcept;
        }

		private string SplitParentKey(string title)
		{
			string[] wordsFromTitle = title.Split(Resources.Resource.Delimeter.ToCharArray());
			return wordsFromTitle[0];
		}
	}
}