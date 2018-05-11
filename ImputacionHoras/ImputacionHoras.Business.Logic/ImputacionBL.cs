using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DataAccessCsv;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (ContractorsDictionary.ContainsKey(entrada.Usuario))
                    entrada.Contractor = ContractorsDictionary[entrada.Usuario];
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
                if ((salidaImputacion.RelatedProject != "") && (salidaImputacion.RelatedProject != "Empty"))
                {
                    BillingConcept = salidaImputacion.RelatedProject;
                }
                else if (salidaImputacion.EpicName != "")
                {
                    BillingConcept = salidaImputacion.EpicName;
                }
                else
                {
                    // Sacar posible ParentKey
                    // Si ParentKey sí existe -> Peticion Api de datos de ParentKey
                    // BC = CalcularBillingConcept(Parent)

                    // Si ParentKey no existe
                    BillingConcept = salidaImputacion.Title;
                }

                BillingConceptDictionary.Add(salidaImputacion.Key, BillingConcept);
            }

            return BillingConcept;
        }

    }
}