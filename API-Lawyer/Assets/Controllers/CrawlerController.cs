using API_Lawyer.Assets.Client;
using API_Lawyer.Assets.Security.autorizacao;
using API_Lawyer.Assets.Services;
using Microsoft.AspNetCore.Authorization;
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
    [RequireAuthentication]
    [Authorize(Policy = "standard")]
    public class CrawlerController : ControllerBase
    {
        private readonly CrawlerClient _crawler;
        private readonly CrawlerService _service;
        public CrawlerController(CrawlerClient crawler, CrawlerService service)
        {
            _crawler = crawler;
            _service = service;
        }

        /// <summary> Busca a página do processo e converte em formato JSON </summary>
        [HttpGet("/search/{numeroProcesso}")]
        public async Task<IActionResult> SearchPage(string numeroProcesso)
        {
            var page = JsonConvert.SerializeObject(_crawler.BaixarPagina(numeroProcesso).Result, Formatting.Indented);
            return Ok(page);    
        }

        /// <summary> Busca na WEB o processo e salva seus dados na base de dados </summary>
        [HttpPost("/salvar/pagina/database/{numeroProcesso}")]
        public async Task<IActionResult> SavePage(string numeroProcesso)
        {
            _service.Start(numeroProcesso);
            return NoContent();
        }
    }
}
