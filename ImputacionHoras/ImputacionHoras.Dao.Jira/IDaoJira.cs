using ImputacionHoras.Common.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.DataAccess.Jira
{
    public interface IDaoJira
    {
        RowImputation GetDataFromParentKey(string parentkey);
        void GenerateCredentials(string usuario, string contraseña);
    }
}
