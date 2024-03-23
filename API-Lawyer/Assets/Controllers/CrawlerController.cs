using API_Lawyer.Assets.Client;
using API_Lawyer.Assets.Services;
using Castle.Core.Internal;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
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
        private readonly CrawlerService _service;
        public CrawlerController(CrawlerClient crawler, CrawlerService service)
        {
            _crawler = crawler;
            _service = service;
        }


        [HttpGet("/search/{numeroProcesso}")]
        public async Task<IActionResult> SearchPage(string numeroProcesso)
        {
            var page = JsonConvert.SerializeObject(_crawler.BaixarPagina(numeroProcesso).Result, Formatting.Indented);
            return Ok(page);    
        }

        /// <summary> Adiciona uma consulta ao banco de dados </summary>
        [HttpPost("/salvar/pagina/database/{numeroProcesso}")]
        public async Task<IActionResult> SavePage(string numeroProcesso)
        {
            _service.Start(numeroProcesso);
            return NoContent();
        }
    }
}
