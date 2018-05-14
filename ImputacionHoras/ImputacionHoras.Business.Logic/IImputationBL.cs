using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Business.Logic
{
    public interface IImputationBL
    {
        void ImportImputations(string pathFile);
        void CalculateContractors(string PathFile);
        void CalculateAllBillingConcepts(string usuario, string contraseña);

    }
}
