using System;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builds = TeamCityAPIClient.ObterBuilsdDaRelease();
            var relatorioCSV = new RelatorioCSV("C:/Temp/DevOps.csv");
            var detalhesBuilds = relatorioCSV.ObterDadosParaRelatorio(builds);
            relatorioCSV.ExportarCSV(detalhesBuilds);

            Console.ReadKey();
        }
    }
}