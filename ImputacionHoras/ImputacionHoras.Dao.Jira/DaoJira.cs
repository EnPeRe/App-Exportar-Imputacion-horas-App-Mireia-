using ImputacionHoras.Common.Logic.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;

namespace ImputacionHoras.DataAccess.Jira
{
    public class DaoJira : IDaoJira
	{
        #region Constructors
        public DaoJira()
		{
		}
        #endregion

        #region Methods
        public RowImputation GetDataFromParentKey(string parentkey, string usuario, string contraseña)
		{
            // Generamos las credenciales codificadas
			var mergedCredentials = string.Format("{0}:{1}", usuario, contraseña);
			var byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
			var encodedCredentials = Convert.ToBase64String(byteCredentials);

			RowImputation rowImputation = new RowImputation();

			using (WebClient webClient = new WebClient())
			{
				webClient.Headers.Set("Authorization", "Basic " + encodedCredentials);

                // Obtenemos la seccion del Json que queremos
				var result = webClient.DownloadString(string.Concat(Resources.JiraResources.WebApiJiraUrl, parentkey));
				var data = (JObject)JsonConvert.DeserializeObject(result);
				var dataFields = data["issues"][0]["fields"];

                // Obtenemos el Title si existe en la seccion del Json
				if (dataFields[Resources.JiraResources.TitleTag] != null)
				{
					string accentedStr = dataFields[Resources.JiraResources.TitleTag].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding(Resources.JiraResources.ISO8859Encoding).GetBytes(accentedStr);
					rowImputation.Title = Encoding.UTF8.GetString(tempBytes);
				}

                // Obtenemos el EpicName si existe en la seccion del Json
                if (dataFields[Resources.JiraResources.EpicNameTag] != null)
				{
					string accentedStr = dataFields[Resources.JiraResources.EpicNameTag].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding(Resources.JiraResources.ISO8859Encoding).GetBytes(accentedStr);
					rowImputation.EpicName = Encoding.UTF8.GetString(tempBytes);
				}

                // Obtenemos el RelatedProject si existe en la seccion del Json
                if (dataFields[Resources.JiraResources.RelatedProjectTag] != null)
				{
					string accentedStr = dataFields[Resources.JiraResources.RelatedProjectTag].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding(Resources.JiraResources.ISO8859Encoding).GetBytes(accentedStr);
					rowImputation.RelatedProject = Encoding.UTF8.GetString(tempBytes);
				}
			}
			return rowImputation;
		}
        #endregion 
    }
}
