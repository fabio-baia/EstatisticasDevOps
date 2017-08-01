using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var detalhesBuilds = new List<BuildDetalhes>();
            var builds = TeamCityAPIClient.ObterBuilsdDaRelease();

            foreach (var b in builds)
            {
                var hora = TeamCityAPIClient.ObterHoraBuild(b.Id);
                var estatisticas = TeamCityAPIClient.ObterPropriedades(b.Id);
                b.Property = estatisticas;

                Console.WriteLine(JsonConvert.SerializeObject(b));

                var nuget = b.Property.Where(x => x.Name == "buildStageDuration:buildStepAg_CSharp_NugetParaTodasAsSolutions_0").Select(x => x.Value).FirstOrDefault();
                var validarArtefatos = b.Property.Where(x => x.Name == "buildStageDuration:buildStepAg_CSharp_ValidarArtefatosWes_0").Select(x => x.Value).FirstOrDefault();
                var validarEntidades = b.Property.Where(x => x.Name == "buildStageDuration:buildStepAg_CSharp_ValidarEntidadesForaDoPadrO_0").Select(x => x.Value).FirstOrDefault();
                var validarReferencias = b.Property.Where(x => x.Name == "buildStageDuration:buildStepAg_CSharp_ValidarReferenciasDlleCom_0").Select(x => x.Value).FirstOrDefault();
                var validarSaidas = b.Property.Where(x => x.Name == "buildStageDuration:buildStepAg_CSharp_ValidarSaDasDosProjetos_0").Select(x => x.Value).FirstOrDefault();
                var validarProjetos = b.Property.Where(x => x.Name == "buildStageDuration:buildStepAg_CSharp_ValidasProjetosNaSolution_0").Select(x => x.Value).FirstOrDefault();
                var checkout = b.Property.Where(x => x.Name == "BuildCheckoutTime").Select(x => x.Value).FirstOrDefault();
                var publicacaoArtefatos = b.Property.Where(x => x.Name == "buildStageDuration:buildStepRUNNER_83").Select(x => x.Value).FirstOrDefault();

                var cSharp = b.Property.Where(x => x.Name.Contains("CSharp_BuildCSharp")).Sum(x => x.Value);

                var detalhe = new BuildDetalhes()
                {
                    IdBuild = b.Id,
                    StartDate = hora,
                    Nuget = new DateTime().Add(TimeSpan.FromMilliseconds(nuget)).ToString("HH:mm:ss"),
                    ValidacaoArtefatos = new DateTime().Add(TimeSpan.FromMilliseconds(validarArtefatos)).ToString("HH:mm:ss"),
                    ValidacaoEntidades = new DateTime().Add(TimeSpan.FromMilliseconds(validarEntidades)).ToString("HH:mm:ss"),
                    ValidacaoReferencias = new DateTime().Add(TimeSpan.FromMilliseconds(validarEntidades)).ToString("HH:mm:ss"),
                    ValidacaoSaidas = new DateTime().Add(TimeSpan.FromMilliseconds(validarSaidas)).ToString("HH:mm:ss"),
                    ValidacaoProjetos = new DateTime().Add(TimeSpan.FromMilliseconds(validarProjetos)).ToString("HH:mm:ss"),
                    Checkout = new DateTime().Add(TimeSpan.FromMilliseconds(checkout)).ToString("HH:mm:ss"),
                    PublicacaoArtefatos = new DateTime().Add(TimeSpan.FromMilliseconds(publicacaoArtefatos)).ToString("HH:mm:ss"),
                    CSharp = new DateTime().Add(TimeSpan.FromMilliseconds(cSharp)).ToString("HH:mm:ss")
                };

                detalhesBuilds.Add(detalhe);
            }

            ExportarCSV(detalhesBuilds);

            Console.ReadKey();
        }

        public static void ExportarCSV(List<BuildDetalhes> detalhesBuilds){
            var csv = new StringBuilder();

            var headers = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}", 
                "Id Build", 
                "Start Date",
                "Nuget", 
                "Validação Artefatos",
                "Validação Entidades", 
                "Validação Referencias", 
                "Validação Saídas", 
                "Validação Projetos", 
                "Checkout",
                "Publicação Artefatos",
                "CSharp");
            csv.AppendLine(headers);

            foreach (var linha in detalhesBuilds)
            {
                var newLine = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}", 
                    linha.IdBuild, 
                    linha.StartDate,
                    linha.Nuget, 
                    linha.ValidacaoArtefatos, 
                    linha.ValidacaoEntidades, 
                    linha.ValidacaoReferencias, 
                    linha.ValidacaoSaidas, 
                    linha.ValidacaoProjetos, 
                    linha.Checkout,
                    linha.PublicacaoArtefatos,
                    linha.CSharp);
                csv.AppendLine(newLine);
            }

            File.WriteAllText("C:/Temp/DevOps.csv", csv.ToString());
        }
    }
}