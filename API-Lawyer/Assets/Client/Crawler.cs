using API_Lawyer.Assets.Model.Processo;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace API_Lawyer.Assets.Client
{
    public class Crawler
    {
        private readonly HttpClient _httpClient;

        public Crawler()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetHtmlAsync(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            return html;
        }

        public ProcessoModel ExtractData(string html)
        {
            Uri url = new Uri("http://esaj.tjba.jus.br/cpo/sg/search.do?paginaConsulta=1&cbPesquisa=NUMPROC&tipoNuProcesso=UNIFICADO&numeroDigitoAnoUnificado=0809979-67.2015&foroNumeroUnificado=0080&dePesquisaNuUnificado=0809979-67.2015.8.05.0080&dePesquisa=&pbEnviar=Pesquisar");
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            string htmlString = client.DownloadString(url);
            HtmlDocument doc23 = new HtmlDocument();
            doc23.LoadHtml(htmlString);
            HtmlNode body23 = doc23.DocumentNode.Element("//body");
            Console.WriteLine(body23);
            //string content23 = body23.InnerText;

            //Console.WriteLine(content23);

            Console.WriteLine("\n------HTML-----\n" + html);


            // Localizar a tabela que contém o valor do processo



            // Extrair número do processo
            //processo.Processo = doc.DocumentNode.SelectSingleNode("//span[@id='Processo']").InnerText;

            // Extrair classe
            //processo.Classe = doc.DocumentNode.SelectSingleNode("//span[@id='Classe']").InnerText;

            // Extrair assunto
            //processo.Assunto = doc.DocumentNode.SelectSingleNode("//span[@id='Relator']").InnerText;

            // Extrair partes
            //processo.Partes = new List<ParteModel>();
            //var partesNodes = doc.DocumentNode.SelectNodes("//div[@class='Destino']");
            //foreach (var node in partesNodes)
            //{
            //var parte = new ParteModel();
            //parte.Nome = node.SelectSingleNode("h3").InnerText;
            //parte.Tipo = node.SelectSingleNode("span[@class='tipoParte']").InnerText;
            //processo.Partes.Add(parte);
            //}

            // Extrair andamentos
            //processo.Andamentos = new List<AndamentoModel>();
            //var andamentosNodes = doc.DocumentNode.SelectNodes("//div[@class='andamentoProcesso']");
            //foreach (var node in andamentosNodes)
            //{
            //var andamento = new AndamentoModel();
            //andamento.Data = node.SelectSingleNode("span[@class='data']").InnerText;
            //andamento.Descricao = node.SelectSingleNode("span[@class='descricao']").InnerText;
            //processo.Andamentos.Add(andamento);
            //}

            return null;
        }

        public string SerializeToJson(ProcessoModel processo)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(processo, options);
            return json;

        }


        public string GerarLinkEsaj(string numeroProcesso)
        {
            string numeroUnificado = numeroProcesso.Substring(0, 15);
            string foro = numeroProcesso.Substring(21);

            // Montando a URL
            string url = "http://esaj.tjba.jus.br/cpo/sg/search.do";
            url += "?paginaConsulta=1";
            url += "&cbPesquisa=NUMPROC";
            url += "&tipoNuProcesso=UNIFICADO";
            url += "&numeroDigitoAnoUnificado=" + numeroUnificado;
            url += "&foroNumeroUnificado=" + foro;
            url += "&dePesquisaNuUnificado=" + numeroProcesso;
            url += "&dePesquisa=";
            url += "&pbEnviar=Pesquisar";

            Console.WriteLine("\nNumeroUnificado: [" + numeroUnificado + "]\nForo: [" + foro + "]\nLink<" + url + ">");

            return url;
        }

    }
}
