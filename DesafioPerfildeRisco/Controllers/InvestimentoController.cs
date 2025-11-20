namespace DesafioPerfildeRisco.Controller;

using DesafioPerfildeRisco.DTOs;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.DbContext;


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class InvestimentoController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IInvestimentoServico _InvestimentoService;

    public InvestimentoController(AppDbContext db, IInvestimentoServico investimentoService)
    {
        _db = db;
        _InvestimentoService = investimentoService;
    }


    //1. Solicitação de Simulação de Investimento

    //POST: api/Investimento/simular-investimento

    [Authorize]
    [HttpPost("simular-investimento")]
    public async Task<IActionResult> Simular([FromBody] SimulacaoRequest request)
    {
        if (request == null)
            return BadRequest("Requisição inválida.");

        var resultado = await _InvestimentoService.SimularInvestimento(request);

        if (resultado == null)
            return NotFound($"Produto de investimento '{request.TipoProduto}' não encontrado.");

        var response = new
        {
            produtoValidado = new
            {
                id = resultado.Produto.IdProduto,
                nome = resultado.Produto.Nome,
                tipo = resultado.Produto.Tipo,
                rentabilidade = resultado.Produto.Rentabilidade,
                risco = resultado.Produto.Risco
            },
            resultadoSimulacao = new
            {
                valorFinal = resultado.ValorFinal,
                rentabilidadeEfetiva = resultado.RentabilidadeEfetiva,
                prazoMeses = resultado.PrazoMeses
            },
            dataSimulacao = resultado.DataSimulacao.ToString("yyyy-MM-ddTHH:mm:ssZ")
        };

        return Ok(response);

    }


    //2. Histórico de Simulações Realizadas
    //    GET:api/Investimento/simulacoes

    [Authorize]
    [HttpGet("simulacoes")]
    public async Task<IActionResult> ListarHistorico()
    {

        var simulacoes = await _db.Simulacoes
            .Include(s => s.Produto)
            .ToListAsync();


        if (!simulacoes.Any())
            return NoContent();

        var response = simulacoes.Select(s => new
        {
            id = s.IdSimulacao,
            clienteId = s.IdCliente,
            produto = s.Produto.Nome,
            valorInvestido = s.ValorInvestido,
            valorFinal = s.ValorFinal,
            prazoMeses = s.PrazoMeses,
            dataSimulacao = s.DataSimulacao.ToString("yyyy-MM-ddTHH:mm:ssZ")
        });

        return Ok(response);
    }



    //3. Valores Simulados por Produto e Dia
    //    GET:api/Investimento/simulacoes/por-produto-dia

    [Authorize]
    [HttpGet("simulacoes/por-produto-dia")]
    public async Task<IActionResult> ListarProdutoPorDia()
    {
        var simulacoes = await _db.Simulacoes
            .Include(s => s.Produto)
            .ToListAsync();

        if (!simulacoes.Any())
            return NoContent();

        var response = simulacoes.GroupBy(s => new { Produto = s.Produto.Nome, Data = s.DataSimulacao.Date })
                        .Select(g => new
                        {
                            produto = g.Key.Produto,
                            data = g.Key.Data.ToString("yyyy-MM-dd"),
                            quantidadeSimulacoes = g.Count(),
                            mediaValorFinal = g.Average(s => s.ValorFinal)
                        });
        return Ok(response);
    }



    //4. Dados de Telemetria
    //    GET:api/Investimento/telemetria

    [AllowAnonymous]
    [HttpGet("telemetria")]
    public IActionResult VerTelemetria([FromServices] AppDbContext db)
    {

        if (!db.Telemetrias.Any())
        {
            return Ok(new
            {
                servicos = new List<object>(),
                periodo = new { inicio = (string?)null, fim = (string?)null }
            });
        }

        var report = db.Telemetrias
            .GroupBy(t => t.NomeServico)
            .Select(g => new
            {
                nome = g.Key,
                quantidadeChamadas = g.Count(),
                mediaTempoRespostaMs = (int)g.Average(x => x.DuracaoMs)
            })
            .ToList();

        var periodo = new
        {
            inicio = db.Telemetrias.Min(t => t.DataHora).ToString("yyyy-MM-dd"),
            fim = db.Telemetrias.Max(t => t.DataHora).ToString("yyyy-MM-dd")
        };

        return Ok(new { servicos = report, periodo });
    }




    //5. Perfil de Risco
    //    GET:api/Investimento/perfil-risco/{clienteId}

    [Authorize]
    [HttpGet("perfil-risco/{clienteId}")]
    public async Task<IActionResult> PerfilDeRisco(int clienteId)
    {
        var investidor = await _db.Investidores.FirstOrDefaultAsync(i => i.IdCliente == clienteId);

        if (investidor == null)
            return NotFound("Investidor não encontrado.");
        else
            return Ok(investidor);
    }


    //6. Produtos Recomendados
    //    GET:api/Investimento/produtos-recomendados/{perfil}

    [AllowAnonymous]
    [HttpGet("produtos-recomendados/{perfil}")]
    public async Task<IActionResult> RecomendacaoPerfil(string perfil)
    {
        if (string.IsNullOrWhiteSpace(perfil))
            return BadRequest("Perfil investidor inválido.");

        if (perfil != "Conservador" && perfil != "Moderado" && perfil != "Agressivo")
            return BadRequest("Perfil investidor inválido.");



        string riscoProduto = perfil == "Conservador" ? "Baixo" :
                              perfil == "Moderado" ? "Médio" : "Alto";

        var response = await _db.Produtos
        .Where(i => i.Risco == riscoProduto)
        .ToListAsync();

        if (!response.Any())
            return NotFound("Nenhum produto encontrado para o perfil especificado.");

        return Ok(response);
    }



    //7. Histórico de Investimentos
    //    GET:api/Investimento/investimentos/{clienteId}

    [Authorize]
    [HttpGet("investimentos/{clienteId}")]
    public async Task<IActionResult> ListarInvestimentosPorCliente(int clienteId)
    {
        var investidor = await _db.Investidores.FirstOrDefaultAsync(i => i.IdCliente == clienteId);

        if (investidor == null)
        {
            return NotFound("Investidor não encontrado.");
        }

        var investimentosCliente = await _db.Simulacoes
        .Include(s => s.Produto)
        .Where(s => s.IdCliente == clienteId)
        .ToListAsync();

        if (!investimentosCliente.Any())
            return NotFound("Nenhum investimento encontrado para o investidor especificado.");

        var response = investimentosCliente.Select(s => new
        {
            id = s.IdSimulacao,
            tipo = s.Produto.Tipo,
            valor = s.ValorInvestido,
            rentabilidade = Math.Round(s.RentabilidadeEfetiva, 2),
            data = s.DataSimulacao.ToString("yyyy-MM-dd")
        });

        return Ok(response);
    }
}