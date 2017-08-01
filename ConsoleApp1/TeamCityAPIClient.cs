using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    public class TeamCityAPIClient
    {
        private string headerAuthorization;
        private string baseEndpoint = "http://mga-tc001:6081/app/rest/";

        public TeamCityAPIClient(string usuario, string senha)
        {
            ConfigurarHeaderAuthorization(usuario, senha);
        }

        private string ConsumirEndpoint(string endpoint)
        {
            var fullEndpoint = baseEndpoint + endpoint;
            var request = (HttpWebRequest)WebRequest.Create(fullEndpoint);
            request.Headers.Add("Authorization", headerAuthorization);
            request.Accept = "application/json";

            var response = request.GetResponse();
            var dataStream = response.GetResponseStream();
            var reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return responseFromServer;
        }

        private void ConfigurarHeaderAuthorization(string usuario, string senha)
        {
            if (usuario == String.Empty || senha == String.Empty)
                throw new Exception("É preciso configurar os dados de acesso ao TeamCity no App.config");

            var authInfo = $"{usuario}:{senha}";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            this.headerAuthorization = "Basic " + authInfo;
        }

        public List<Build> ObterBuilsdDaRelease()
        {
            var response = ConsumirEndpoint("builds/?locator=buildType:Ag_CSharp_Release46");

            var buildConfiguration = JsonConvert.DeserializeObject<BuildConfiguration>(response);
            return buildConfiguration.Build;
        }

        public List<Property> ObterPropriedades(int buildId)
        {
            var endpoint = string.Format("builds/id:{0}/statistics", buildId);
            var response = ConsumirEndpoint(endpoint);

            var build = JsonConvert.DeserializeObject<Build>(response);
            return build.Property;
        }

        public string ObterHoraBuild(int buildId)
        {
            var endpoint = string.Format("builds/id:{0}", buildId);
            var response = ConsumirEndpoint(endpoint);

            var build = JsonConvert.DeserializeObject<Build>(response);
            return build.StartDate;
        }
    }
}