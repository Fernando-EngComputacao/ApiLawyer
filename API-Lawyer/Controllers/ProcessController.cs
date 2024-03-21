using API_Lawyer.Client;
using Microsoft.AspNetCore.Mvc;

namespace API_Lawyer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessController : ControllerBase
    {
        private readonly Crawler _crawler;
        public ProcessController(Crawler crawler) 
        { 
            _crawler = crawler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var numProcesso = "0809979-67.2015.8.05.0080";
            var link = _crawler.GerarLinkEsaj(numProcesso);
            var html = await _crawler.GetHtmlAsync(link);
            var processo = _crawler.ExtractData(html);
            var json = _crawler.SerializeToJson(processo);

            return Ok(json);
        }
    }
}
