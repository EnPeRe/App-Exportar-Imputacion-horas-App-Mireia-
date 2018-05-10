using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Common.Logic.Modelo
{
    public class SalidaImputacion
    {
        public string Key { get; set; }
        public string Proyecto { get; set; }
        public string Tipo { get; set; }
        public string Title { get; set; }
        public string BillingConcept { get; set; }
        public string Asset { get; set; }
        public string EpicName { get; set; }
        //public string ParentKey { get; set; }
        public string RelatedProject { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaImputacion { get; set; }
        public float HorasImputadas { get; set; }

        public SalidaImputacion()
        {
        }

        public SalidaImputacion(string key, string proyecto, string tipo, string title, string billingConcept, string asset, string epicName, string relatedProject, string usuario, DateTime fechaImputacion, float horasImputadas)
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
        }

        public override string ToString()
        {
            return string.Concat(Proyecto, "\t",
                                Tipo, "\t",
                                Key, "\t",
                                Title, "\t",
                                BillingConcept, ".\t",
                                Asset, ".\t",
                                EpicName, ".\t",
                                RelatedProject, ".\t",
                                FechaImputacion.ToShortDateString(), "\t",
                                Usuario, "\t",
                                HorasImputadas.ToString());
        }

        public override bool Equals(object obj)
        {
            var imputacion = obj as SalidaImputacion;
            return imputacion != null &&
                   Key == imputacion.Key &&
                   Proyecto == imputacion.Proyecto &&
                   Tipo == imputacion.Tipo &&
                   Title == imputacion.Title &&
                   BillingConcept == imputacion.BillingConcept &&
                   Asset == imputacion.Asset &&
                   EpicName == imputacion.EpicName &&
                   RelatedProject == imputacion.RelatedProject &&
                   Usuario == imputacion.Usuario &&
                   FechaImputacion == imputacion.FechaImputacion &&
                   HorasImputadas == imputacion.HorasImputadas;
        }

        public override int GetHashCode()
        {
            var hashCode = 588575609;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Proyecto);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Tipo);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BillingConcept);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Asset);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EpicName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RelatedProject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Usuario);
            hashCode = hashCode * -1521134295 + FechaImputacion.GetHashCode();
            hashCode = hashCode * -1521134295 + HorasImputadas.GetHashCode();
            return hashCode;
        }
    }
}
