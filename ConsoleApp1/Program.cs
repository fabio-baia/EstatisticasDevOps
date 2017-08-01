using System;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builds = TeamCityAPIClient.ObterBuilsdDaRelease();

            var detalhesBuilds = RelatorioCSV.ObterDadosParaRelatorio(builds);
            RelatorioCSV.ExportarCSV(detalhesBuilds);

            Console.ReadKey();
        }
    }
}