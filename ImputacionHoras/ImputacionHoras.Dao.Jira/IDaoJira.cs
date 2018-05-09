using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.DaoJira
{
    public interface IDaoJira
    {
        List<EntradaImputacion> GetData(string usuario, string contraseña, DateTime FromDate, DateTime ToDate);
    }
}
