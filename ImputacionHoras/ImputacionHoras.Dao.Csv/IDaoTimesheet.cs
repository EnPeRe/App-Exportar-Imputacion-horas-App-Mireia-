using ImputacionHoras.Common.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.DataAccess.Timesheet
{
    public interface IDaoTimesheet
    {
        List<RowImputation> ImportImputationsFromCsv(string pathFile);
        void ExportImputationsToCsv(List<RowImputation> imputationsList);
        List<DataContractor> ImportDataContractorsFromCsv(string pathFile);

        List<RowImputation> ImportImputationsFromXls(string pathFile);
        List<DataContractor> ImportDataContractorsFromXls(string pathFile);
    }
}
