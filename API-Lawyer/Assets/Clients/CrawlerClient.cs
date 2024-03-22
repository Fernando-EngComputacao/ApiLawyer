using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace API_Lawyer.Assets.Client
{
    public class CrawlerClient
    {
        private readonly CrawlerValidator _validator;

        public CrawlerClient()
        {
            _validator = new CrawlerValidator();
        }

        public async Task<Dictionary<string, string>> BaixarPagina(string numeroProcesso)
        {
            // Validar o numeroProcesso 
            ValidarRequestCrawler(numeroProcesso);

            var url = GerarLink(numeroProcesso);
            using (var httpClient = new HttpClient())
            {
                var httpResponse = await httpClient.GetAsync(url);
                var htmlBytes = await httpResponse.Content.ReadAsByteArrayAsync();
                var htmlString = Encoding.GetEncoding("iso-8859-1").GetString(htmlBytes);

                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlString);

                var result = new Dictionary<string, string>();

                var nuProcessoNodes = htmlDocument.DocumentNode.SelectNodes("//td");

                if (nuProcessoNodes != null)
                {
                    int counter = 1;
                    foreach (var node in nuProcessoNodes)
                    {
                        string key = $"Info_{counter}";
                        string value = Regex.Replace(Regex.Replace(Regex.Replace(node.InnerText, @"\s+", " ").Trim(), "&gt;", "").Trim(), "&nbsp;", "").Trim();
                        if (!string.IsNullOrEmpty(value) && !result.ContainsValue(value))
                        {
                            result.Add(key, value);
                            counter++;
                        }
                    }
                }

                return result;
            }
        }

        private string GerarLink(string numeroProcesso)
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

            return url;
        }

        private void ValidarRequestCrawler(string numeroProcesso)
        {
            var validationResult = _validator.Validate(numeroProcesso);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(Environment.NewLine, validationResult.Errors);
                throw new LawyerException(errors, HttpStatusCode.BadRequest);
            }
        }
    }
}