using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioPerfildeRisco.Controller;
using DesafioPerfildeRisco.DbContext;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.DTOs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

public class InvestimentoControllerTests
{
    private readonly AppDbContext _db;
    private readonly Mock<IInvestimentoServico> _mockService;
    private readonly InvestimentoController _controller;

    public InvestimentoControllerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // banco isolado por teste
            .Options;

        _db = new AppDbContext(options);
        _mockService = new Mock<IInvestimentoServico>();
        _controller = new InvestimentoController(_db, _mockService.Object);
    }


    //1. Solicitação de Simulação de Investimento

    [Fact]
    public async Task TestarSe_Simular_RequestValido_RetornaOk()
    {
        var request = new SimulacaoRequest { ValorInvestido = 1000, PrazoMeses = 12 };
        var simulacao = new Simulacao
        {
            Produto = new Produto(1,"CDB","Renda Fixa",0.1m,"Baixo"),
            ValorFinal = 1100,
            RentabilidadeEfetiva = 0.1m,
            PrazoMeses = 12,
            DataSimulacao = DateTime.UtcNow
        };

        _mockService.Setup(s => s.SimularInvestimento(request)).ReturnsAsync(simulacao);

        var result = await _controller.Simular(request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }


    //2. Histórico de Simulações Realizadas
    [Fact]
    public async Task TestarSe_ListarHistorico_QuandoNaoHaSimulacoes_RetornaNoContent()
    {
        var result = await _controller.ListarHistorico();
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task TestarSe_ListarHistorico_QuandoExistemSimulacoes_RetornaOk()
    {
        _db.Simulacoes.Add(new Simulacao
        {
            IdSimulacao = 1,
            IdCliente = 123,
            Produto = new Produto(1,"CDB","Renda Fixa",0.1m,"Baixo"),
            ValorInvestido = 1000,
            ValorFinal = 1100,
            PrazoMeses = 12,
            DataSimulacao = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        var result = await _controller.ListarHistorico();
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }


    //3. Valores Simulados por Produto e Dia
    [Fact]
    public async Task TestarSe_ListarProdutoPorDia_QuandoNaoHaSimulacoes_RetornaNoContent()
    {
        var result = await _controller.ListarProdutoPorDia();
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task TestarSe_ListarProdutoPorDia_QuandoExistemSimulacoes_RetornaOk()
    {
        _db.Simulacoes.Add(new Simulacao
        {
            IdSimulacao = 1,
            IdCliente = 123,
            Produto = new Produto(1,"CDB","Renda Fixa",0.1m,"Baixo"),
            ValorInvestido = 1000,
            ValorFinal = 1100,
            PrazoMeses = 12,
            DataSimulacao = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        var result = await _controller.ListarProdutoPorDia();
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }


    //4. Dados de Telemetria
    [Fact]
    public void TestarSe_VerTelemetria_QuandoNaoHaDados__RetornaOk()
    {
        var result = _controller.VerTelemetria(_db);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    [Fact]
    public void TestarSe_VerTelemetria_QuandoExistemDados_RetornaOk()
    {
        _db.Telemetrias.Add(new Telemetria { Id=1, NomeServico="Simulacao", DuracaoMs=100, DataHora=DateTime.UtcNow });
        _db.SaveChanges();

        var result = _controller.VerTelemetria(_db);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }

    //5. Perfil de Risco
    [Fact]
    public async Task TestarSe_PerfilDeRisco_InvestidorNaoExiste_RetornaNotFound()
    {
        var result = await _controller.PerfilDeRisco(999);
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task TestarSe_PerfilDeRisco_InvestidorExiste_RetornaOk()
    {
        _db.Investidores.Add(new Investidor(1));
        await _db.SaveChangesAsync();

        var result = await _controller.PerfilDeRisco(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }




//6. Produtos Recomendados
[Fact]
public async Task TestarSe_RecomendacaoPerfil_PerfilNuloOuVazio_RetornaBadRequest()
{
    var result = await _controller.RecomendacaoPerfil("");
    Assert.IsType<BadRequestObjectResult>(result);
}

[Fact]
public async Task TestarSe_RecomendacaoPerfil_PerfilInvalido_RetornaBadRequest()
{
    var result = await _controller.RecomendacaoPerfil("Invalido");
    Assert.IsType<BadRequestObjectResult>(result);
}

[Fact]
public async Task TestarSe_RecomendacaoPerfil_NaoExistemProdutosParaPerfil_RetornaNotFound()
{
    // Banco vazio, nenhum produto com risco "Baixo"
    var result = await _controller.RecomendacaoPerfil("Conservador");
    Assert.IsType<NotFoundObjectResult>(result);
}

[Fact]
public async Task TestarSe_RecomendacaoPerfil_ExistemProdutosParaConservador_RetornaOk()
{
    _db.Produtos.Add(new Produto(1,"CDB","Renda Fixa",0.1m,"Baixo"));
    await _db.SaveChangesAsync();

    var result = await _controller.RecomendacaoPerfil("Conservador");
    var okResult = Assert.IsType<OkObjectResult>(result);
    var produtos = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
    Assert.Single(produtos);
}

[Fact]
public async Task TestarSe_RecomendacaoPerfil_ExistemProdutosParaModerado_RetornaOk()
{
    _db.Produtos.Add(new Produto(2,"Fundo Moderado","Multimercado",0.15m,"Médio"));
    await _db.SaveChangesAsync();

    var result = await _controller.RecomendacaoPerfil("Moderado");
    var okResult = Assert.IsType<OkObjectResult>(result);
    var produtos = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
    Assert.Single(produtos);
}

[Fact]
public async Task TestarSe_RecomendacaoPerfil_ExistemProdutosParaAgressivo_RetornaOk()
{
    _db.Produtos.Add(new Produto(3,"Ações","Renda Variável",0.25m,"Alto"));
    await _db.SaveChangesAsync();

    var result = await _controller.RecomendacaoPerfil("Agressivo");
    var okResult = Assert.IsType<OkObjectResult>(result);
    var produtos = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
    Assert.Single(produtos);
}





    // 7. Investimentos por Cliente
    [Fact]
    public async Task TestarSe_ListarInvestimentosPorCliente_QuandoInvestidorNaoExiste_RetornaNotFound()
    {
        var result = await _controller.ListarInvestimentosPorCliente(999);
        Assert.IsType<NotFoundObjectResult>(result);
    }



    [Fact]
    public async Task TestarSe_ListarInvestimentosPorCliente_ExistemInvestimentos_RetornaOk()
    {
        var investidor = new Investidor(1);
        _db.Investidores.Add(investidor);

        _db.Simulacoes.Add(new Simulacao
        {
            IdSimulacao = 1,
            IdCliente = 1,
            Produto = new Produto(1,"CDB","Renda Fixa",0.1m,"Baixo"),
            ValorInvestido = 1000,
            ValorFinal = 1100,
            PrazoMeses = 12,
            DataSimulacao = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();

        var result = await _controller.ListarInvestimentosPorCliente(1);
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);
    }
}


