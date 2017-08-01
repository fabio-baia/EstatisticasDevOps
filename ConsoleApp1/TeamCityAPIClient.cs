using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    public static class TeamCityAPIClient
    {
        private static string ConsumirEndpoint(string endpoint)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Headers.Add("Authorization", ObterBasicHeaderAuthorization());
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer;
        }

        private static string ObterBasicHeaderAuthorization()
        {
            var usuario = ConfigurationManager.AppSettings["usuarioTeamCity"];
            var senha = ConfigurationManager.AppSettings["senhaTeamCity"];

            if (usuario == String.Empty || senha == String.Empty)
                throw new Exception("É preciso configurar os dados de acesso ao TeamCity no App.config");

            string authInfo = $"{usuario}:{senha}";

            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            return "Basic " + authInfo;
        }

        public static List<Build> ObterBuilsdDaRelease()
        {
            var response = ConsumirEndpoint("http://mga-tc001:6081/app/rest/builds/?locator=buildType:Ag_CSharp_Release46");

            Coisa coisa = JsonConvert.DeserializeObject<Coisa>(response);
            return coisa.Build;
        }

        public static List<Property> ObterPropriedades(int buildId)
        {
            var endpoint = string.Format("http://mga-tc001:6081/app/rest/builds/id:{0}/statistics", buildId);
            var response = ConsumirEndpoint(endpoint);

            Build build = JsonConvert.DeserializeObject<Build>(response);
            return build.Property;
        }

        public static string ObterHoraBuild(int buildId)
        {
            var endpoint = string.Format("http://mga-tc001:6081/app/rest/builds/id:{0}", buildId);
            var response = ConsumirEndpoint(endpoint);

            Build build = JsonConvert.DeserializeObject<Build>(response);
            return build.StartDate;
        }
    }
}