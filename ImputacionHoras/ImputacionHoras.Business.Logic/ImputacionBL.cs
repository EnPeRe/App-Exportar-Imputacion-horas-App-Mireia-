using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DataAccessCsv;
using ImputacionHoras.DataAccessJira;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public List<RowImputacion> ListaImputaciones { get; set; }
        public Dictionary<string, string> BillingConceptDictionary { get; set; }
        public Dictionary<string, string> ContractorsDictionary { get; set; }
        public int Contador = 0;

        public ImputacionBL()
        {
            DataAccessCsv = new DaoCsv();
            DataAccessJira = new DaoJira();
            ListaImputaciones = new List<RowImputacion>();
            BillingConceptDictionary = new Dictionary<string, string>();
            ContractorsDictionary = new Dictionary<string, string>();
        }

        public ImputacionBL(DaoCsv dataAccessCsv, DaoJira dataAccessJira, List<RowImputacion> listaImputacionesIn, Dictionary<string, string> billingConceptDictionary, Dictionary<string, string> contractorsDictionary)
        {
            DataAccessCsv = dataAccessCsv;
			DataAccessJira = dataAccessJira;
            ListaImputaciones = listaImputacionesIn;
            BillingConceptDictionary = billingConceptDictionary;
            ContractorsDictionary = contractorsDictionary;
        }

        public void ImportarImputaciones(string pathFile)
        {
            this.ListaImputaciones = DataAccessCsv.ImportarCsvImputaciones(pathFile);
        }

		public void CalcularDiccionarioContractors(string pathFile)
		{
            List<DataDevelopers> dataDevelopers = new List<DataDevelopers>();
            dataDevelopers = DataAccessCsv.ImportarCsvDataDevelopers(pathFile);
            foreach (var entrada in dataDevelopers)
                ContractorsDictionary.Add(entrada.JiraUser, entrada.Contractor);
		}

        public void CalcularContractors()
        {
            foreach(var entrada in ListaImputaciones)
            {
                if (ContractorsDictionary.ContainsKey(entrada.Creator))
                    entrada.Contractor = ContractorsDictionary[entrada.Creator];
            }
        }
        
		public void CalcularBCs()
        {
            foreach (var imputacion in ListaImputaciones)
            {
                imputacion.BillingConcept = CalcularBillingConcept(imputacion);
            }
                
        }

        public string CalcularBillingConcept(RowImputacion salidaImputacion)
        {
            var BillingConcept = Resources.Resource.NullText;

            if (BillingConceptDictionary.ContainsKey(salidaImputacion.Key))
            {
                BillingConcept = BillingConceptDictionary[salidaImputacion.Key];
            }
            else
            {
                Contador++;
                if ((salidaImputacion.RelatedProject != "") && (salidaImputacion.RelatedProject != Resources.Resource.EmptyText))
                {
                    BillingConcept = salidaImputacion.RelatedProject;
                }
                else if (salidaImputacion.EpicName != "")
                {
                    BillingConcept = salidaImputacion.EpicName;
                }
                else

				{
					string parentKey = SplitParentKey(salidaImputacion.Title);
					if (salidaImputacion.Title != parentKey && parentKey.Length < 15)

					{
						RowImputacion rowImputacionParentKey = DataAccessJira.GetDataFromParentKey(parentKey, "", "");
						BillingConcept = CalcularBillingConcept(rowImputacionParentKey);
						rowImputacionParentKey.Key = salidaImputacion.Key;
					}
					else
					{
						BillingConcept = salidaImputacion.Title;
					}
                    // Sacar posible ParentKey
                    // Si ParentKey sí existe -> Peticion Api de datos de ParentKey
                    // BC = CalcularBillingConcept(Parent)

                    // Si ParentKey no existe
                   
                }

                BillingConceptDictionary.Add(salidaImputacion.Key, BillingConcept);
            }

            return BillingConcept;
        }

		private string SplitParentKey(string title)
		{
			string[] wordsFromTitle = title.Split(Resources.Resource.Delimeter.ToCharArray());
			return wordsFromTitle[0];
		}
	}
	
}