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
        public List<EntradaImputacion> ListaImputacionesIn { get; set; }
        public List<SalidaImputacion> ListaImputacionesOut { get; set; }
        public Dictionary<string, string> BillingConceptDictionary { get; set; }
        public int contador = 0;

        public ImputacionBL()
        {
            DataAccessCsv = new DaoCsv();
            ListaImputacionesIn = new List<EntradaImputacion>();
            ListaImputacionesOut = new List<SalidaImputacion>();
            BillingConceptDictionary = new Dictionary<string, string>();
        }

        public ImputacionBL(DaoCsv dataAccessCsv, List<EntradaImputacion> listaImputacionesIn, List<SalidaImputacion> listaImputacionesOut, Dictionary<string, string> billingConceptDictionary)
        {
            DataAccessCsv = dataAccessCsv;
            ListaImputacionesIn = listaImputacionesIn;
            ListaImputacionesOut = listaImputacionesOut;
            BillingConceptDictionary = billingConceptDictionary;
        }

        public void ImportarImputaciones(string pathFile)
        {
            this.ListaImputacionesIn = DataAccessCsv.ImportarExcelImputaciones(pathFile);
        }
        
        public void CalcularSalidas()
        {
            foreach (var imputacion in ListaImputacionesIn)
                ListaImputacionesOut.Add(ConvertirImputacion(imputacion));
        }

        public SalidaImputacion ConvertirImputacion(EntradaImputacion entradaImputacion)
        {
            SalidaImputacion salidaImputacion = new SalidaImputacion();

            salidaImputacion.Key = entradaImputacion.Key;
            salidaImputacion.Proyecto = entradaImputacion.Proyecto;
            salidaImputacion.Tipo = entradaImputacion.Tipo;
            salidaImputacion.Title = entradaImputacion.Title;
            salidaImputacion.EpicName = entradaImputacion.EpicName;
            salidaImputacion.RelatedProject = entradaImputacion.RelatedProject;
            salidaImputacion.Usuario = entradaImputacion.Usuario;
            salidaImputacion.FechaImputacion = entradaImputacion.FechaImputacion;
            salidaImputacion.HorasImputadas = entradaImputacion.HorasImputadas;


            salidaImputacion.BillingConcept = CalcularBillingConcept(salidaImputacion);
            salidaImputacion.Asset = "";

            return salidaImputacion;
        }

        public string CalcularBillingConcept(SalidaImputacion salidaImputacion)
        {
            string BillingConcept = "null";

            if (BillingConceptDictionary.ContainsKey(salidaImputacion.Key))
            {
                BillingConcept = BillingConceptDictionary[salidaImputacion.Key];
            }
            else
            {
                contador++;
                if ((salidaImputacion.RelatedProject != "") && (salidaImputacion.RelatedProject != "Empty"))
                {
                    BillingConcept = salidaImputacion.RelatedProject;
                }
                else if (salidaImputacion.Tipo.Equals("Epic"))
                {
                    BillingConcept = salidaImputacion.EpicName;
                }
                else
                {
                    BillingConcept = salidaImputacion.Title;
                }

                BillingConceptDictionary.Add(salidaImputacion.Key, BillingConcept);
            }

            return BillingConcept;
        }

    }
}