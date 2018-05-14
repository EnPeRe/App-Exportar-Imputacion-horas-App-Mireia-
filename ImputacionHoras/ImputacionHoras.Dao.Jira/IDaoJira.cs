using ImputacionHoras.Common.Logic.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.DataAccess.Jira
{
    public interface IDaoJira
    {
        RowImputation GetDataFromParentKey(string parentkey, string usuario, string contraseña);
    }
}
