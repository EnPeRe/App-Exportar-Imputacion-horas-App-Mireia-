using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DataAccess.Timesheet;
using ImputacionHoras.DataAccess.Jira;
using System.Collections.Generic;

namespace ImputacionHoras.Business.Logic
{
    public class ImputationBL
    {
        #region Properties
        private readonly DaoTimesheet DataAccessTimesheet;
        private readonly DaoJira DataAccessJira;
        public List<RowImputation> ImputationsList { get; set; }
        public Dictionary<string, string> BillingConceptDictionary { get; set; }
        public Dictionary<string, string> ContractorsDictionary { get; set; }
        public int Counter = 0;
        #endregion

        #region Constructors
        public ImputationBL()
        {
            DataAccessTimesheet = new DaoTimesheet();
            DataAccessJira = new DaoJira();
            ImputationsList = new List<RowImputation>();
            BillingConceptDictionary = new Dictionary<string, string>();
            ContractorsDictionary = new Dictionary<string, string>();
        }

        public ImputationBL(DaoTimesheet dataAccessCsv, DaoJira dataAccessJira, List<RowImputation> listaImputaciones, Dictionary<string, string> billingConceptDictionary, Dictionary<string, string> contractorsDictionary, int contador)
        {
            DataAccessTimesheet = dataAccessCsv;
            DataAccessJira = dataAccessJira;
            ImputationsList = listaImputaciones;
            BillingConceptDictionary = billingConceptDictionary;
            ContractorsDictionary = contractorsDictionary;
            Counter = contador;
        }
        #endregion

        #region Methods Imputations
        public void ImportImputations(string pathFile)
        {
            this.ImputationsList = DataAccessTimesheet.ImportImputationsFromCsv(pathFile);
        }
        #endregion

        #region Methods Contractors
        public void CalculateContractors(string PathFile)
        {
            CalculateContractorDictionary(PathFile);

            foreach (var row in ImputationsList)
            {
                if (ContractorsDictionary.ContainsKey(row.Creator))
                    row.Contractor = ContractorsDictionary[row.Creator];
            }
        }

        private void CalculateContractorDictionary(string pathFile)
        {
            List<DataContractor> dataContractors = new List<DataContractor>();
            dataContractors = DataAccessTimesheet.ImportDataContractorsFromCsv(pathFile);
            foreach (var row in dataContractors)
                ContractorsDictionary.Add(row.JiraUser, row.Contractor);
        }
        #endregion

        #region Methods BillingConcept
        public void CalculateAllBillingConcepts(string usuario, string contraseña)
        {
            foreach (var row in ImputationsList)
            {
                row.BillingConcept = CalculateSingleBillingConcept(row, usuario, contraseña);
            }
        }
        
        //Usamos BillingConceptDictionary para optimizar el numero de llamadas realizadas a la API de Jira
        //BillingConcept toma el siguiente valor:
        //1.- RelatedProject si existe
        //2.- Si no existe, EpicName si existe
        //3.- Si no existe, BillingConcept del padre
        //4.- Si no hay padre, Title propio
        private string CalculateSingleBillingConcept(RowImputation rowImputation, string usuario, string contraseña)
        {
            var billingConcept = string.Empty;

            if (BillingConceptDictionary.ContainsKey(rowImputation.Key))
            {
                billingConcept = BillingConceptDictionary[rowImputation.Key];
            }
            else
            {

                if ((rowImputation.RelatedProject != string.Empty) && (rowImputation.RelatedProject != Resources.BusinessResources.EmptyLiteralText))
                {
                    billingConcept = rowImputation.RelatedProject;
                }
                else if (rowImputation.EpicName != string.Empty)
                {
                    billingConcept = rowImputation.EpicName;
                }
                else
				{
					string parentKey = GetParentKey(rowImputation.Title);
					if (rowImputation.Title != parentKey && parentKey.Length < 15)
					{
						RowImputation rowImputationParent = DataAccessJira.GetDataFromParentKey(parentKey, usuario, contraseña);
						billingConcept = CalculateSingleBillingConcept(rowImputationParent, usuario, contraseña);
                        // Añadimos la key y BC del parent al diccionario para no tener que volver a buscarlo
                        if (!BillingConceptDictionary.ContainsKey(rowImputationParent.Key))
                            BillingConceptDictionary.Add(rowImputationParent.Key, billingConcept);
					}
					else
					{
						billingConcept = rowImputation.Title;
					}
                }
                // Añadimos la key y BC de la row al diccionario para no tener que volver a buscarlo
                if (!BillingConceptDictionary.ContainsKey(rowImputation.Key))
                    BillingConceptDictionary.Add(rowImputation.Key, billingConcept);
            }

            return billingConcept;
        }

        private string GetParentKey(string title)
        {
            string[] wordsFromTitle = title.Split(Resources.BusinessResources.Delimeter.ToCharArray());
            return wordsFromTitle[0];
        }
        #endregion
    }
}