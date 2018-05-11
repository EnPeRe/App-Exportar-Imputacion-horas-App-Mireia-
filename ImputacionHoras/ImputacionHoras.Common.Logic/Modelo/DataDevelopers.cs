using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImputacionHoras.Common.Logic.Modelo
{
	public class DataDevelopers
	{
		public string JiraUser { get; set; }
		public string Contractor { get; set; }

        public DataDevelopers()
        {
        }

        public DataDevelopers(string jiraUser, string contractor)
        {
            JiraUser = jiraUser;
            Contractor = contractor;
        }
    }
}
