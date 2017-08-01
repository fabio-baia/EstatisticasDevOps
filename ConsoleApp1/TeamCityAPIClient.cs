using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    public static class TeamCityAPIClient
    {
        public static List<Build> ObterBuilsdDaRelease()
        {
            string authInfo = "usuario" + ":" + "senha";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://mga-tc001:6081/app/rest/builds/?locator=buildType:Ag_CSharp_Release46");
            request.Headers.Add("Authorization", "Basic " + authInfo);
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Coisa coisa = JsonConvert.DeserializeObject<Coisa>(responseFromServer);

            reader.Close();
            response.Close();

            return coisa.Build;
        }

        public static List<Property> ObterPropriedades(int buildId)
        {
            string authInfo = "usuario" + ":" + "senha";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            var url = string.Format("http://mga-tc001:6081/app/rest/builds/id:{0}/statistics", buildId);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Basic " + authInfo);
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Build coisa = JsonConvert.DeserializeObject<Build>(responseFromServer);

            reader.Close();
            response.Close();

            return coisa.Property;
        }

        public static string ObterHoraBuild(int buildId)
        {
            string authInfo = "usuario" + ":" + "senha";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            var url = string.Format("http://mga-tc001:6081/app/rest/builds/id:{0}", buildId);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("Authorization", "Basic " + authInfo);
            request.Accept = "application/json";

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            Build coisa = JsonConvert.DeserializeObject<Build>(responseFromServer);

            //DateTimeOffset result = DateTimeOffset.Parse(coisa.StartDate, CultureInfo.GetCultures(CultureTypes.AllCultures).Where(x => x.));

            reader.Close();
            response.Close();

            return coisa.StartDate;
        }
    }
}