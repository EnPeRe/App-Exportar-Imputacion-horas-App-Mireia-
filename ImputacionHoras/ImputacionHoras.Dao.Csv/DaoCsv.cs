using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace ImputacionHoras.Dao.Csv
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
