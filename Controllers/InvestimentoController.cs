
using DesafioPerfilInvestidor.DTOs;
using DesafioPerfilInvestidor.Models;
using DesafioPerfilInvestidor.Services;
using DesafioPerfilInvestidor.MockDB;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InvestimentoController : ControllerBase
{
    // private readonly AppDbContext _db;
    private readonly IInvestimentoServico _InvestimentoService;

    public InvestimentoController(IInvestimentoServico investimentoService)
    {
        //     _db = db;
        _InvestimentoService = investimentoService;
    }


    //1. Solicitação de Simulação de Investimento
    //POST: api/simular-investimento

    [HttpPost("simular-investimento")]
    public IActionResult Simular([FromBody] SimulacaoRequest request)
    {

        Simulacao resultado = _InvestimentoService.SimularInvestimento(request);


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
                rentabilidadeEfetiva = resultado.VerRentabilidade(),
                prazoMeses = resultado.PrazoMeses
            },
            dataSimulacao = resultado.DataSimulacao.ToString("yyyy-MM-ddTHH:mm:ssZ")
        };

        return Ok(response);

    }


    //2. Histórico de Simulações Realizadas
    //    GET:api/simulacoes

    [HttpGet("simulacoes")]
    public IActionResult ListarHistorico()
    {
        if (DataBase.Simulacoes.Count == 0)
        {
            return NoContent();
        }

        var response = DataBase.Simulacoes.Select(s => new
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
    //    GET:api/simulacoes/por-produto-dia

    [HttpGet("simulacoes/por-produto-dia")]
    public IActionResult ListarProdutoPorDia()
    {
        if (DataBase.Simulacoes.Count == 0)
        {
            return NoContent();
        }
        var response = DataBase.Simulacoes.GroupBy(s => new { Produto = s.Produto.Nome, Data = s.DataSimulacao.Date })
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
    //    GET:api/telemetria

    [HttpGet("telemetria")]
    public IActionResult VerTelemetria()
    {        
        return Ok("Aguardando implementação de telemetria...");
    }



    //5. Perfil de Risco
    //    GET:api/perfil-risco/{clienteId}
        [HttpGet("perfil-risco/{clienteId}")]
    public IActionResult PerfilDeRisco(int clienteId)
    {
        var investidor = DataBase.Investidores.Find(i => i.IdCliente == clienteId);

        if (investidor == null)
            return NotFound("Investidor não encontrado.");
        else
            return Ok(investidor);
    }


    //6. Produtos Recomendados
    //    GET:api/produtos-recomendados/{perfil}
        [HttpGet("produtos-recomendados/{perfil}")]
    public IActionResult RecomendacaoPerfil(string perfil)
    {
        if (string.IsNullOrWhiteSpace(perfil))
            return BadRequest("Perfil investidor inválido.");
        if (perfil != "Conservador" && perfil != "Moderado" && perfil != "Agressivo")
            return BadRequest("Perfil investidor inválido.");


        
        string riscoProduto = perfil == "Conservador" ? "Baixo" : perfil == "Moderado" ? "Médio" : "Alto";

        var response = DataBase.Produtos.FindAll(i => i.Risco == riscoProduto);
        if (response == null || response.Count == 0)
            return NotFound();
        else
            return Ok(response);
    }



    //7. Histórico de Investimentos
    //    GET:api/investimentos/{clienteId}

    [HttpGet("investimentos/{clienteId}")]
    public IActionResult ListarInvestimentosPorCliente(int clienteId)
    {   
        
        if (!DataBase.Investidores.Any(i => i.IdCliente == clienteId))
        {
            return NotFound("Investidor não encontrado.");
        }
        var investimentosCliente = DataBase.Simulacoes.Where(s => s.IdCliente == clienteId).ToList();

        if (investimentosCliente == null || investimentosCliente.Count == 0)
            return NotFound("Nenhum investimento encontrado para o investidor especificado.");

        var response = investimentosCliente.Select(s => new
        {
            id = s.IdSimulacao,
            tipo = s.Produto.Tipo,
            valor = s.ValorInvestido,
            rentabilidade = s.VerRentabilidade(),
            data = s.DataSimulacao.ToString("yyyy-MM-dd")
        });

        return Ok(response);
 
    }







    }