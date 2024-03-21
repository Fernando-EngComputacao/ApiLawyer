using API_Lawyer.Assets.Client;
using Castle.Core.Internal;

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
        private readonly CrawlerClient _crawler;
        public CrawlerController(CrawlerClient crawler)
        {
            _crawler = crawler;
        }

        /// <summary> Busca os dados da página web e salva na base de dados </summary>
        [HttpPost("/search/{numeroProcesso}")]
        public async Task<IActionResult> SearchPage(string numeroProcesso)
        {
            var page = JsonConvert.SerializeObject(_crawler.BaixarPagina(numeroProcesso).Result, Formatting.Indented);
            return Ok(page);
        }
    }
}
