using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.DataAccessCsv
{
    public interface IDaoCsv
    {
        void ExportarExcelImputaciones(List<EntradaImputacion> listaImputaciones);
        List<EntradaImputacion> ImportarExcelImputaciones(string pathFile);
    }
}
