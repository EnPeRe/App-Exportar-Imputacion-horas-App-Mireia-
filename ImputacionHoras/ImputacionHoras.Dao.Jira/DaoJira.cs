using ImputacionHoras.Common.Logic.CustomExceptions;
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
        public string EncodedCredentials { get; set; }

        #region Constructors
        public DaoJira()
		{
		}
        #endregion

        #region Methods
        public void GenerateCredentials(string usuario, string contraseña)
        {
            try
            {
                var mergedCredentials = string.Format("{0}:{1}", usuario, contraseña);
                var byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
                this.EncodedCredentials = Convert.ToBase64String(byteCredentials);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }
        }

        public void CheckCredentials()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Set("Authorization", "Basic " + EncodedCredentials);
                    // Obtenemos la seccion del Json que queremos
                    webClient.DownloadString(string.Concat(Resources.JiraResources.WebApiJiraUrl, "CREW-38"));
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }
        }

        public RowImputation GetDataFromParentKey(string parentkey)
		{
			RowImputation rowImputation = new RowImputation();

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Set("Authorization", "Basic " + EncodedCredentials);

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
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, ex.InnerException);
            }

			return rowImputation;
		}
        #endregion 
    }
}
