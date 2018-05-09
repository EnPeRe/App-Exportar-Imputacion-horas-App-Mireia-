using ImputacionHoras.Common.Logic.Modelo;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ImputacionHoras.DaoCsv
{
    public class DaoCsv : IDaoCsv
    {
        string PathFile;

        public DaoCsv()
        {
            this.PathFile = string.Concat(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                                    @"/imputaciones.csv");
        }

        public void ExportarImputaciones(List<EntradaImputacion> listaImputaciones)
        {
            using (StreamWriter sw = File.AppendText(PathFile))
            {
                sw.WriteLine("sep=,");
                foreach (var row in listaImputaciones)
                {
                    sw.WriteLine(row.ToString());
                }
            }
        }


    }
}
