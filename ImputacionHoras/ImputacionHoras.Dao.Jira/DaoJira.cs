using ImputacionHoras.Common.Logic.Modelo;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImputacionHoras.DataAccessJira
{
    public class DaoJira : IDaoJira
	{
		public DaoJira()
		{
		}

		public RowImputation GetDataFromParentKey(string parentkey, string usuario, string contraseña)
		{
			var mergedCredentials = string.Format("{0}:{1}", usuario, contraseña);
			var byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
			var encodedCredentials = Convert.ToBase64String(byteCredentials);
			RowImputation rowImputacion = new RowImputation();
			using (WebClient webClient = new WebClient())
			{
				webClient.Headers.Set("Authorization", "Basic " + encodedCredentials);

				var result = webClient.DownloadString(string.Concat("https://jira.vueling.com/rest/api/2/search?jql=key=", parentkey));
				var data = (JObject)JsonConvert.DeserializeObject(result);
				var dataFields = data["issues"][0]["fields"];

				if (dataFields["summary"] != null)
				{
					string accentedStr = dataFields["summary"].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
					rowImputacion.Title = Encoding.UTF8.GetString(tempBytes);
				}


				if (dataFields["customfield_10474"] != null)
				{
					string accentedStr = dataFields["customfield_10474"].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
					rowImputacion.EpicName = Encoding.UTF8.GetString(tempBytes);
				}


				if (dataFields["customfield_17171"] != null)
				{
					string accentedStr = dataFields["customfield_17171"].Value<string>();
					byte[] tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(accentedStr);
					rowImputacion.RelatedProject = Encoding.UTF8.GetString(tempBytes);
				}

			}
			return rowImputacion;
		}

	}
}
