using API_Lawyer.Assets.Client;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace API_Lawyer.Assets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrawlerController : ControllerBase
    {
        private readonly Crawler _crawler;
        public CrawlerController(Crawler crawler)
        {
            _crawler = crawler;
        }

        /// <summary> Busca os dados da página web e salva na base de dados </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var numProcesso = "0809979-67.2015.8.05.0080";
            //var link = _crawler.GerarLinkEsaj(numProcesso);
            //var html = await _crawler.GetHtmlAsync(link);
            //var processo = _crawler.ExtractData(html);
            //var json = _crawler.SerializeToJson(processo);

            //return Ok(json);

            string url = "http://esaj.tjba.jus.br/cpo/sg/search.do?paginaConsulta=1&cbPesquisa=NUMPROC&tipoNuProcesso=UNIFICADO&numeroDigitoAnoUnificado=0809979-67.2015&foroNumeroUnificado=0080&dePesquisaNuUnificado=0809979-67.2015.8.05.0080&dePesquisa=&pbEnviar=Pesquisar";

            //var httpClient = new HttpClient();
            //var html = await httpClient.GetStringAsync(url);
            //var htmlDocument = new HtmlDocument();
            //htmlDocument.LoadHtml(html);

            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(url);

            var htmlBytes = await httpResponse.Content.ReadAsByteArrayAsync();
            var htmlString = Encoding.GetEncoding("iso-8859-1").GetString(htmlBytes);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            var result = new Dictionary<string, string>();
            var resultTd = new Dictionary<string, string>();

            var nuProcessoNodes = htmlDocument.DocumentNode.SelectNodes("//span");
            var nuProcessoNodesTd = htmlDocument.DocumentNode.SelectNodes("//td");
            if (nuProcessoNodes != null)
            {
                int counter = 1;
                foreach (var node in nuProcessoNodes)
                {
                    string key = $"NoId_{counter++}"; // Aqui você pode definir o nome do campo
                    string value = node.InnerText.Trim(); // Aqui você extrai o valor do campo
                    result.Add(key, value);
                }
            }

            if (nuProcessoNodesTd != null)
            {
                int counter = 1;
                foreach (var node in nuProcessoNodesTd)
                {
                    string key = $"NoId_{counter++}"; // Aqui você pode definir o nome do campo
                    string value = Regex.Replace(Regex.Replace(Regex.Replace(node.InnerText, @"\s+", " ").Trim(), "&gt;", "").Trim(), "&nbsp;", "").Trim(); // Aqui você extrai o valor do campo
                    resultTd.Add(key, value);
                }
            }

            // Depois de extrair todos os dados necessários, você pode serializar o resultado para JSON
            string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
            string jsonResultTd = JsonConvert.SerializeObject(resultTd, Formatting.Indented);

            //Console.WriteLine("\n-------- SPAN --------\n" + jsonResult);
            //Console.WriteLine("\n-------- TD --------\n" + jsonResultTd);
            Console.WriteLine("[TD] "+result["NoId_4"]);
            Console.WriteLine("\n[SPAN] "+resultTd["NoId_4"]);


            return Ok(resultTd);
        }
    }
}
