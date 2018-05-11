using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DataAccessCsv;
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
        public List<RowImputacion> ListaImputaciones { get; set; }
        public Dictionary<string, string> BillingConceptDictionary { get; set; }
        public Dictionary<string, string> ContractorsDictionary { get; set; }
        public int Contador = 0;

        public ImputacionBL()
        {
            DataAccessCsv = new DaoCsv();
            ListaImputaciones = new List<RowImputacion>();
            BillingConceptDictionary = new Dictionary<string, string>();
            ContractorsDictionary = new Dictionary<string, string>();
        }

        public ImputacionBL(DaoCsv dataAccessCsv, List<RowImputacion> listaImputacionesIn, Dictionary<string, string> billingConceptDictionary, Dictionary<string, string> contractorsDictionary)
        {
            DataAccessCsv = dataAccessCsv;
            ListaImputaciones = listaImputacionesIn;
            BillingConceptDictionary = billingConceptDictionary;
            ContractorsDictionary = contractorsDictionary;
        }

        public void ImportarImputaciones(string pathFile)
        {
            this.ListaImputaciones = DataAccessCsv.ImportarExcelImputaciones(pathFile);
        }

		public void CalcularDiccionarioContractors(string pathFile)
		{
            List<DataDevelopers> dataDevelopers = new List<DataDevelopers>();
            dataDevelopers = DataAccessCsv.ImportarExcelDataDevelopers(pathFile);
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
					if (salidaImputacion.Title != parentKey)

					{
						RowImputacion rowImputacionParentKey = GetDataFromParentKey(parentKey);
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

		private RowImputacion GetDataFromParentKey(string parentkey)
		{
			var mergedCredentials = string.Format("{0}:{1}", "", "");
			var byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
			var encodedCredentials = Convert.ToBase64String(byteCredentials);
			RowImputacion rowImputacion = new RowImputacion();
			using (WebClient webClient = new WebClient())
			{
				webClient.Headers.Set("Authorization", "Basic " + encodedCredentials);

				var result = webClient.DownloadString("https://jira.vueling.com/rest/api/2/search?jql=key="+parentkey);
				var data = (JObject)JsonConvert.DeserializeObject(result);
				var dataFields = data["issues"][0]["fields"];
					
				if (dataFields["summary"] != null)
				{
					string accentedStr = dataFields["summary"].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
					rowImputacion.Title = Encoding.UTF8.GetString(tempBytes);
				}
				

				if (dataFields["customfield_10474"] != null)
				{
					string accentedStr = dataFields["customfield_10474"].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
					rowImputacion.EpicName = Encoding.UTF8.GetString(tempBytes);
				}
				

				if (dataFields["customfield_17171"] != null)
				{
					string accentedStr = dataFields["customfield_17171"].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
					rowImputacion.RelatedProject = Encoding.UTF8.GetString(tempBytes);
				}
					
			}
			return rowImputacion;
		}

		private string SplitParentKey(string title)
		{
			string[] wordsFromTitle = title.Split(Resources.Resource.Delimeter.ToCharArray());
			return wordsFromTitle[0];
		}
	}
	
}