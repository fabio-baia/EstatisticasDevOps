namespace ConsoleApp1.Models
{
    public class BuildDetalhes
    {
        public int IdBuild { get; set; }
        public string Nuget { get; internal set; }
        public string ValidacaoArtefatos { get; internal set; }
        public string ValidacaoEntidades { get; internal set; }
        public string ValidacaoReferencias { get; internal set; }
        public string ValidacaoSaidas { get; internal set; }
        public string ValidacaoProjetos { get; internal set; }
        public string Checkout { get; internal set; }
        public string StartDate { get; internal set; }
        public string CSharp { get; internal set; }
        public string PublicacaoArtefatos { get; internal set; }
    }
}