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
        public string RelatedProject { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaImputacion { get; set; }
        public float HorasImputadas { get; set; }
        public string Contractor { get; set; }
		public string Creator { get; set; }

        public RowImputacion()
        {
            Key = "";
            Proyecto = "";
            Tipo = "";
            Title = "";
            BillingConcept = "";
            Asset = "";
            EpicName = "";
            RelatedProject = "";
            Usuario = "";
            Contractor = "";
			Creator = "";
        }

        public RowImputacion(string key, string proyecto, string tipo, string title, string billingConcept, string asset, string epicName, string relatedProject, string usuario, DateTime fechaImputacion, float horasImputadas, string contractor, string creator)
        {
            Key = key;
            Proyecto = proyecto;
            Tipo = tipo;
            Title = title;
            BillingConcept = billingConcept;
            Asset = asset;
            EpicName = epicName;
            RelatedProject = relatedProject;
            Usuario = usuario;
            FechaImputacion = fechaImputacion;
            HorasImputadas = horasImputadas;
            Contractor = contractor;
			Creator = creator;
        }

        public override string ToString()
        {
            return string.Concat(Proyecto, "\t",
                                Tipo, "\t",
                                Key, "\t",
                                Title, "\t",
                                BillingConcept, "\t BillingConcept",
                                Asset, "\t Asset",
                                EpicName, "\t",
                                RelatedProject, "RelatedProject\t",
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString(), "\t",
                                Contractor.ToString() + "\t",
                                Creator, "\t");
        }

        public string ToStringIn()
        {
            return string.Concat(Proyecto, "\t",
                                Tipo, "\t",
                                Key, "\t",
                                Title, "\t",
                                EpicName, "\t",
                                RelatedProject, "\t",
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString(),
								Creator, "\t"
                                );
        }

        public string ToStringOut()
        {
            return string.Concat(Contractor.ToString(), "\t",
								Proyecto, "\t",
								"BC" + BillingConcept, "\t",
								"Asset" + Asset, "\t",
								Tipo, "\t",
                                Key, "\t",
                                Title, "\t",                               
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString(), "\t");
        }
    }
}
