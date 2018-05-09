using ImputacionHoras.Common.Logic.Modelo;
using ImputacionHoras.DaoCsv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Presentation.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<EntradaImputacion> listaImputaciones = new List<EntradaImputacion>();
            IDaoCsv daoCsv = new DaoCsv();

            EntradaImputacion row1 = new EntradaImputacion("Key", "Area", "Asset", "Usuario", Convert.ToDateTime("12-1-2018"),TimeSpan.Parse("10:20"), "Empresa");
            EntradaImputacion row2 = new EntradaImputacion("qwerqwerq", "weqrqer", "qewrqwe", "qwerqre", Convert.ToDateTime("12-1-2018"), TimeSpan.Parse("10:20"), "Vueling");
            

            listaImputaciones.Add(row1);
            listaImputaciones.Add(row2);

            daoCsv.ExportarImputaciones(listaImputaciones);
        }
    }
}
