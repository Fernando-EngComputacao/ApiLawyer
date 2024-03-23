using API_Lawyer.Assets.Client;
using API_Lawyer.Assets.Data;
using API_Lawyer.Assets.Model.Movimentacao.dto;
using API_Lawyer.Assets.Model.Origem.dto;
using API_Lawyer.Assets.Model.Processo.dto;
using API_Lawyer.Assets.Models.Transicao.dto;
using API_Lawyer.Assets.Services.Interfaces;
using API_Lawyer.Assets.Services.Validators;
using API_Lawyer.Exceptions;
using API_Lawyer.Model;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace API_Lawyer.Assets.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly CrawlerClient _crawlerClient;
        private readonly MovimentacaoService _movimetacao;
        private readonly OrigemService _origem;
        private readonly ProcessoService _processo;
        private readonly TransicaoService _transicao;
        private readonly IMapper _mapper;
        private readonly LawyerContext _context;
        private readonly CrawlerValidator _validator;

        public CrawlerService(CrawlerClient crawlerClient, 
                                MovimentacaoService movimentacao, 
                                OrigemService origem,
                                ProcessoService processo,
                                TransicaoService transicao,
                                IMapper mapper,
                                LawyerContext context,
                                CrawlerValidator validator)
        {
            _crawlerClient = crawlerClient;
            _movimetacao = movimentacao;
            _origem = origem;
            _processo = processo;
            _transicao = transicao;
            _mapper = mapper;
            _context = context;
            _validator = validator;
        }

        public void Start(string numeroProcesso)
        {
            // Validar o numeroProcesso 
            ValidarRequestCrawler(numeroProcesso);
            if (ValidaExisteCadastro(numeroProcesso))
            {
                new LawyerException("Número do processo já cadastrado", HttpStatusCode.BadRequest);
            }

            var page = _crawlerClient.BaixarPagina(numeroProcesso).Result;
            //Processo
            var processoId = SalvarProcesso(_crawlerClient.ProcessoJson(page));
            //Movimento
            SalvarMovimentacao(_crawlerClient.MovimentacaoJson(page), numeroProcesso, processoId.Result);
            //Origem e Transicao
            SalvarOrigemETransicao(_crawlerClient.OrigemJson(page), processoId.Result);
        }

        
        public async Task<int?> SalvarProcesso(Dictionary<string, string> dados)
        {
            int idProcesso = (int)(_processo.GetIdAsync().Result != null ? _processo.GetIdAsync().Result : 0);
            idProcesso++;
            CreateProcessoDTO dto = new CreateProcessoDTO();
            dto.NumeroProcesso = dados["NumeroProcesso"];
            dto.Classe = dados["Classe"];
            dto.Area = dados["Área"];
            dto.Assunto = dados["Assunto"];
            dto.Distribuicao = dados["Distribuição"];
            dto.Relator = dados["Relator"];
            dto.Volume = dados["Volume"];

            //***Pronto
            var processo = _mapper.Map<Processo>(dto);
            processo.Ativo = 1;
            _context.Processos.Add(processo);
            await _context.SaveChangesAsync();

            //Console.WriteLine(JsonConvert.SerializeObject(processo, Formatting.Indented));

            return idProcesso;
        }

        public void SalvarOrigemETransicao(Dictionary<string, string> dados, int? processoId)
        {
            int idOrigem = (int)(_origem.GetIdAsync().Result != null ? _origem.GetIdAsync().Result : 0);
            string[] partes = dados["Origem"].Split('/');
            
            foreach (var item in partes)
            {
                CreateOrigemDTO dtoOrigem = new CreateOrigemDTO();
                dtoOrigem.Local = item;
                var origem = _mapper.Map<Origem>(dtoOrigem);
                origem.Ativo = 1;
                //***Pronto
                if (!_context.Origens.Any(o => o.Local == dtoOrigem.Local))
                {
                    _context.Origens.Add(origem);
                    _context.SaveChanges();
                }

                var catOrigem = _context.Origens.FirstOrDefault(x => x.Local == origem.Local);
                int idOrigemValido = catOrigem.Id;
                CreateTransicaoDTO dtoTransicao = new CreateTransicaoDTO();
                dtoTransicao.ProcessoId = processoId;
                dtoTransicao.OrigemId = idOrigemValido;

                //***Pronto
                var transicao = _mapper.Map<Transicao>(dtoTransicao);
                transicao.Ativo = 1;
                _context.Transicoes.Add(transicao);
                _context.SaveChanges();

                //Console.WriteLine(JsonConvert.SerializeObject(dtoOrigem, Formatting.Indented));
                //Console.WriteLine(JsonConvert.SerializeObject(_mapper.Map<CreateTransicaoDbDTO>(transicao), Formatting.Indented));
            }



        }


        private void SalvarMovimentacao(Dictionary<string, string> dados, string numeroProcesso, int? processoId)
        {
            for (int count = 0;  count < dados.Count/2; count++)
            {
                CreateMovimentacaoDTO movimentacaoDto = new CreateMovimentacaoDTO();
                movimentacaoDto.Descricao = dados["Descricao_" + count];
                string data = dados["Data_" + count];
                string dataFormatada = $"{data.Substring(6, 4)}-{data.Substring(3, 2)}-{data.Substring(0, 2)}";
                movimentacaoDto.Date = DateTime.ParseExact(dataFormatada, "yyyy-MM-dd", null);
                movimentacaoDto.ProcessoId = processoId;

                //***Pronto
                var movimentacao = _mapper.Map<Movimentacao>(movimentacaoDto);
                movimentacao.Ativo = 1;
                _context.Movimentacoes.Add(movimentacao);
                _context.SaveChanges();

                //Console.WriteLine(JsonConvert.SerializeObject(movimentacaoDto, Formatting.Indented));
            }
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

        private bool ValidaExisteCadastro(string numeroProcesso)
        {
            // Verifica se o número do processo já existe no banco de dados
            return _context.Processos.All(p => p.NumeroProcesso == numeroProcesso);
        }
    }
}
