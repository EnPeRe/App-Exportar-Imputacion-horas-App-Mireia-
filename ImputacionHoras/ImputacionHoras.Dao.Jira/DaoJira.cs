using System;
using System.Collections.Generic;
using ImputacionHoras.Common.Logic.Modelo;
using Atlassian.Jira;

namespace ImputacionHoras.DaoJira
{
    public class DaoJira : IDaoJira
    {
        public List<EntradaImputacion> GetData(string usuario, string contraseña, DateTime FromDate, DateTime ToDate)
        {
            var jiraConn = Jira.CreateRestClient(Resources.urlJira, usuario, contraseña);
            var listaEntradas = new List<EntradaImputacion>();

            var FromDateFormatted = FromDate.ToShortDateString().Replace("/","-");
            var ToDateFormatted = ToDate.ToShortDateString().Replace("/", "-");

            var issues = from i in jiraConn.Issues.Queryable
                         where i.Resolved > new LiteralMatch(FromDateFormatted) && i.Resolved < new LiteralMatch(ToDateFormatted)
                         orderby i.Created
                         select i;


        }
    }
}
