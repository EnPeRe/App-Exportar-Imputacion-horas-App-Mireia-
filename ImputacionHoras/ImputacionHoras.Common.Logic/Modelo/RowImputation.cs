using System;

namespace ImputacionHoras.Common.Logic.Modelo
{
    public class RowImputation
    {
        public string Key { get; set; }
        public string Project { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string BillingConcept { get; set; }
        public string Asset { get; set; }
        public string EpicName { get; set; }
        public string RelatedProject { get; set; }
        public string PersonName { get; set; }
        public DateTime ImputationDate { get; set; }
        public float ImputedHours { get; set; }
        public string Contractor { get; set; }
		public string Creator { get; set; }

        public RowImputation()
        {
            Key = "";
            Project = "";
            Type = "";
            Title = "";
            BillingConcept = "";
            Asset = "";
            EpicName = "";
            RelatedProject = "";
            PersonName = "";
            Contractor = "";
			Creator = "";
        }

        public RowImputation(string key, string project, string type, string title, string billingConcept, string asset, string epicName, string relatedProject, string personName, DateTime imputationDate, float imputedHours, string contractor, string creator)
        {
            Key = key;
            Project = project;
            Type = type;
            Title = title;
            BillingConcept = billingConcept;
            Asset = asset;
            EpicName = epicName;
            RelatedProject = relatedProject;
            PersonName = personName;
            ImputationDate = imputationDate;
            ImputedHours = imputedHours;
            Contractor = contractor;
            Creator = creator;
        }

        public override string ToString()
        {
            return string.Concat(Project, "\t",
                                Type, "\t",
                                Key, "\t",
                                Title, "\t",
                                "BC: " + BillingConcept, "\t ",
                                "Asset: " + Asset, "\t",
                                EpicName, "\t",
                                "RP:" + RelatedProject, "\t",
                                ImputationDate.ToShortDateString(), "\t",
                                PersonName, "\t",
                                ImputedHours.ToString(), "\t",
                                Contractor.ToString() + "\t",
                                Creator, "\t");
        }

        public string ToStringIn()
        {
            return string.Concat(Project, "\t",
                                Type, "\t",
                                Key, "\t",
                                Title, "\t",
                                EpicName, "\t",
                                RelatedProject, "\t",
                                ImputationDate.ToShortDateString(), "\t",
                                PersonName, "\t",
                                ImputedHours.ToString(),
								Creator, "\t"
                                );
        }

        public string ToStringOut()
        {
            return string.Concat(Contractor, "\t",
								Project, "\t",
								"BC:" + BillingConcept, "\t",
								"Asset:" + Asset, "\t",
								Type, "\t",
                                Key, "\t",
                                Title, "\t",
                                ImputationDate.ToShortDateString(), "\t",
                                PersonName, "\t",
                                ImputedHours.ToString(), "\t");
        }
    }
}
