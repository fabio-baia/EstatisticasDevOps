using System;
using System.Configuration;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ConfigurationManager.AppSettings;

            var teamCityApiClient = new TeamCityAPIClient(config["usuarioTeamCity"], config["senhaTeamCity"]);
            var relatorioCSV = new RelatorioCSV(config["diretorioCSV"], teamCityApiClient);

            var builds = teamCityApiClient.ObterBuilsdDaRelease();
            var detalhesBuilds = relatorioCSV.ObterDadosParaRelatorio(builds);

            relatorioCSV.ExportarCSV(detalhesBuilds);

            Console.ReadKey();
        }
    }
}