using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using API_Lawyer.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace API_Lawyer.Assets.Client
{
    public class CrawlerClient
    {
        private readonly CrawlerValidator _validator;

        public CrawlerClient(CrawlerValidator validator)
        {
            _validator = validator;
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
                //ProcessoJson(result);
                OrigemJson(result);
                return result;
            }
        }

        private Dictionary<string, string> ProcessoJson(Dictionary<string, string> dado)
        {
            Dictionary<string, string> processo = new Dictionary<string, string>();
            string[] valores = {
            "Números de origem",
            "Classe",
            "Área",
            "Assunto",
            "Distribuição",
            "Relator",
            "Volume"
            };


            for (int count = 0; count < valores.Length; count++)
            {
                dado.Where(kv => kv.Value.Contains(valores[count]) && kv.Value.Length < 19)
                .ToList()
                .ForEach(kv =>
                {
                   switch(count)
                    {
                        case 0:
                            processo.Add("NumeroProcesso", dado["Info_" + (int.Parse(kv.Key.Substring(5)) + 1)]);
                            break;
                        case 2:
                            processo.Add(valores[count], dado["Info_" + (int.Parse(kv.Key.Substring(5)))].Substring(6));
                            break;
                        case 6:
                            processo.Add(valores[count], dado["Info_" + (int.Parse(kv.Key.Substring(5)) + 1)].Substring(0,1));
                            break;
                        default:
                            processo.Add(valores[count], dado["Info_" + (int.Parse(kv.Key.Substring(5)) + 1)]);
                            break;
                    }
                });
            }
            Console.WriteLine($"Process \n {JsonConvert.SerializeObject(processo, Formatting.Indented)}");
            return processo;
        }


        private Dictionary<string, string> OrigemJson(Dictionary<string, string> dado)
        {
            string texto = "Origem";
            Dictionary<string, string> origem = new Dictionary<string, string>();
            dado.Where(kv => kv.Value == $"{texto}:" && kv.Value.Length == 7)
                .ToList()
                .ForEach(kv => origem.Add($"{texto}", dado["Info_" + (int.Parse(kv.Key.Substring(5)) + 1)]));

            Console.WriteLine($"Origem \n {JsonConvert.SerializeObject(origem, Formatting.Indented)}");
            return new Dictionary<string, string>();
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