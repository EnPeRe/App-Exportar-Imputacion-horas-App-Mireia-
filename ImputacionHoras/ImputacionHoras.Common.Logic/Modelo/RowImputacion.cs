using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Common.Logic.Modelo
{
    public class RowImputacion
    {
        public string Key { get; set; }
        public string Proyecto { get; set; }
        public string Tipo { get; set; }
        public string Title { get; set; }
        public string BillingConcept { get; set; }
        public string Asset { get; set; }
        public string EpicName { get; set; }
        public string ParentLink { get; set; }
        public string RelatedProject { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaImputacion { get; set; }
        public float HorasImputadas { get; set; }
        public string Contractor { get; set; }

        public RowImputacion()
        {
            Key = "";
            Proyecto = "";
            Tipo = "";
            Title = "";
            BillingConcept = "";
            Asset = "";
            EpicName = "";
            ParentLink = "";
            RelatedProject = "";
            Usuario = "";
            Contractor = "";
        }

        public RowImputacion(string key, string proyecto, string tipo, string title, string billingConcept, string asset, string epicName, string parentLink, string relatedProject, string usuario, DateTime fechaImputacion, float horasImputadas, string contractor)
        {
            Key = key;
            Proyecto = proyecto;
            Tipo = tipo;
            Title = title;
            BillingConcept = billingConcept;
            Asset = asset;
            EpicName = epicName;
            ParentLink = parentLink;
            RelatedProject = relatedProject;
            Usuario = usuario;
            FechaImputacion = fechaImputacion;
            HorasImputadas = horasImputadas;
            Contractor = contractor;
        }

        public override string ToString()
        {
            return string.Concat(Proyecto, "\t",
                                Tipo, "\t",
                                Key, "\t",
                                Title, "\t",
                                BillingConcept, ".\t",
                                Asset, "\t",
                                EpicName, "\t",
                                ParentLink, "\t",
                                RelatedProject, ".\t",
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString(), "\t",
                                Contractor.ToString() + ".");
        }

        public string ToStringIn()
        {
            return string.Concat(Proyecto, "\t",
                                Tipo, "\t",
                                Key, "\t",
                                Title, "\t",
                                EpicName, "\t",
                                ParentLink, "\t",
                                RelatedProject, "\t",
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString()
                                );
        }

        public string ToStringOut()
        {
            return string.Concat(Proyecto, "\t",
                                Tipo, "\t",
                                Key, "\t",
                                Title, "\t",
                                BillingConcept, "\t",
                                Asset, "\t",
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString(), "\t",
                                Contractor.ToString());
        }
    }
}
