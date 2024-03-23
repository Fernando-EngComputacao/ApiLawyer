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
        public CrawlerClient()
        {
        }

        public async Task<Dictionary<string, string>> BaixarPagina(string numeroProcesso)
        {
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
                ProcessoJson(result);
                return result;
            }
        }

        public Dictionary<string, string> ProcessoJson(Dictionary<string, string> dado)
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
            //Console.WriteLine($"\n {JsonConvert.SerializeObject(processo, Formatting.Indented)}");
            return processo;
        }


        public Dictionary<string, string> OrigemJson(Dictionary<string, string> dado)
        {
            string texto = "Origem";
            Dictionary<string, string> origem = new Dictionary<string, string>();
            dado.Where(kv => kv.Value == $"{texto}:" && kv.Value.Length == 7)
                .ToList()
                .ForEach(kv => origem.Add($"{texto}", dado["Info_" + (int.Parse(kv.Key.Substring(5)) + 1)]));

            //Console.WriteLine($"Origem \n {JsonConvert.SerializeObject(origem, Formatting.Indented)}");
            return origem;
        }

        public Dictionary<string, string> MovimentacaoJson(Dictionary<string, string> dados)
        {
            Dictionary<string, string> movimento = new Dictionary<string, string>();

            bool inMovimento = false;
            string descricao = "";
            string currentData = null;
            int count = 0;
            foreach (var kvp in dados)
            {
                if (kvp.Value == "Movimento" && kvp.Value.Length == 9)
                {
                    inMovimento = true;
                }
                else if (kvp.Value.Contains("Não há incidentes, ações incidentais, recursos") && kvp.Value.Length == 46)
                {
                    break;
                }
                else if (DateTime.TryParseExact(kvp.Value, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
                {
                    
                    if (currentData != null)
                    {
                        if (!string.IsNullOrEmpty(descricao))
                        {
                            movimento.Add("Data_"+count,currentData);
                            movimento.Add("Descricao_"+count, descricao.Substring(3));
                            descricao = ""; count++;
                        }
                    }
                    currentData = kvp.Value;
                }
                else if (inMovimento)
                {
                    descricao += " / " + kvp.Value;
                }
            }

            //Console.WriteLine($"Movimentacao \n {JsonConvert.SerializeObject(movimento, Formatting.Indented)}");
            return movimento;
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
    }
}