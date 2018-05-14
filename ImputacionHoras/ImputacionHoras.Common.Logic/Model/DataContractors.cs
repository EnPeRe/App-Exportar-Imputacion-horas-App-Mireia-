namespace ImputacionHoras.Common.Logic.Model
{
    public class DataContractor
	{
		public string JiraUser { get; set; }
		public string Contractor { get; set; }

        public DataContractor()
        {
        }

        public DataContractor(string jiraUser, string contractor)
        {
            JiraUser = jiraUser;
            Contractor = contractor;
        }
    }
}
