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
        void ExportarExcelImputaciones(List<RowImputacion> listaImputaciones);
        List<RowImputacion> ImportarExcelImputaciones(string pathFile);
    }
}
