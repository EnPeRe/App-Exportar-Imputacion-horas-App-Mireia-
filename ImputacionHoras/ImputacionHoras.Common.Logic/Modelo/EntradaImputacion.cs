using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Common.Logic.Modelo
{
    public class EntradaImputacion
    {
        string Key { get; set; }
        string Area { get; set; }
        string Asset { get; set; }
        string Usuario { get; set; }
        DateTime FechaImputacion { get; set; }
        TimeSpan HorasImputadas { get; set; }
        string Empresa { get; set; }

        public EntradaImputacion(string key, string area, string asset, string usuario, DateTime fechaImputacion, TimeSpan horasImputadas, string empresa)
        {
            Key = key;
            Area = area;
            Asset = asset;
            Usuario = usuario;
            FechaImputacion = fechaImputacion;
            HorasImputadas = horasImputadas;
            Empresa = empresa;
        }

        public override bool Equals(object obj)
        {
            var imputacion = obj as EntradaImputacion;
            return imputacion != null &&
                   Key == imputacion.Key &&
                   Area == imputacion.Area &&
                   Asset == imputacion.Asset &&
                   Usuario == imputacion.Usuario &&
                   FechaImputacion == imputacion.FechaImputacion &&
                   HorasImputadas.Equals(imputacion.HorasImputadas) &&
                   Empresa == imputacion.Empresa;
        }

        public override int GetHashCode()
        {
            var hashCode = -409086513;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Key);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Area);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Asset);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Usuario);
            hashCode = hashCode * -1521134295 + FechaImputacion.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TimeSpan>.Default.GetHashCode(HorasImputadas);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Empresa);
            return hashCode;
        }

        public override string ToString()
        {
            return string.Concat(Key, ",",
                                Area, ",",
                                Asset, ",",
                                Usuario, ",",
                                FechaImputacion.ToShortDateString(), ",",
                                HorasImputadas.ToString(), ",",
                                Empresa);
        }
    }
}
