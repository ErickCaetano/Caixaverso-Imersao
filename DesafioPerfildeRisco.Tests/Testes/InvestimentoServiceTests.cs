namespace DesafioPerfildeRisco.Tests;

using Xunit;
using Microsoft.EntityFrameworkCore;
using DesafioPerfildeRisco.DbContext;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.DTOs;
using DesafioPerfildeRisco.Services;
using System;
using System.Threading.Tasks;

public class InvestimentoServiceTests
{
    private readonly AppDbContext _db;
    private readonly MotorDeRecomendacao _motor;
    private readonly InvestimentoService _service;

    public InvestimentoServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _db = new AppDbContext(options);
        _motor = new MotorDeRecomendacao(_db);
        _service = new InvestimentoService(_db, _motor);
    }



    [Fact]
    public async Task TestarSe_SimularInvestimento_DadosInvalidos_DeveLancarExcecao()
    {
        var request = new SimulacaoRequest
        {
            IdCliente = 0,
            TipoProduto = "",
            ValorInvestido = 0,
            PrazoMeses = 0
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _service.SimularInvestimento(request));
    }



    [Fact]
    public async Task TestarSe_SimularInvestimento_ProdutoNaoExiste_DeveLancarExcecao()
    {
        var request = new SimulacaoRequest
        {
            IdCliente = 1,
            TipoProduto = "CDB",
            ValorInvestido = 1000,
            PrazoMeses = 12
        };

        await Assert.ThrowsAsync<ArgumentException>(() => _service.SimularInvestimento(request));
    }



    [Fact]
    public async Task TestarSe_SimularInvestimento_QuandoRequestValido_DeveRetornarSimulacao()
    {
        _db.Produtos.Add(new Produto(1, "CDB", "CDB", 0.12m, "Baixo"));
        await _db.SaveChangesAsync();

        var request = new SimulacaoRequest
        {
            IdCliente = 1,
            TipoProduto = "CDB",
            ValorInvestido = 1000,
            PrazoMeses = 12
        };

        var simulacao = await _service.SimularInvestimento(request);

        Assert.NotNull(simulacao);
        Assert.Equal(request.IdCliente, simulacao.IdCliente);
        Assert.Equal(request.ValorInvestido, simulacao.ValorInvestido);
        Assert.True(simulacao.ValorFinal > request.ValorInvestido);
    }



    [Fact]
    public async Task TestarSe_SimularInvestimento_InvestidorNaoExiste_DeveCriarInvestidor()
    {
        _db.Produtos.Add(new Produto(1, "CDB", "CDB", 0.12m, "Baixo"));
        await _db.SaveChangesAsync();

        var request = new SimulacaoRequest
        {
            IdCliente = 99,
            TipoProduto = "CDB",
            ValorInvestido = 1000,
            PrazoMeses = 12
        };

        var simulacao = await _service.SimularInvestimento(request);

        var investidor = await _db.Investidores.FirstOrDefaultAsync(i => i.IdCliente == 99);
        Assert.NotNull(investidor);
        Assert.Equal("Desconhecido", investidor.PerfilDeRisco);
    }



    [Fact]
    public async Task TestarSe_SimularInvestimento_InvestidorJaExiste_DeveAtualizarInvestidor()
    {
        _db.Produtos.Add(new Produto(1, "CDB", "CDB", 0.12m, "Baixo"));
        _db.Investidores.Add(new Investidor(1));
        await _db.SaveChangesAsync();

        var request = new SimulacaoRequest
        {
            IdCliente = 1,
            TipoProduto = "CDB",
            ValorInvestido = 1000,
            PrazoMeses = 12
        };

        var simulacao = await _service.SimularInvestimento(request);

        var investidor = await _db.Investidores.FirstOrDefaultAsync(i => i.IdCliente == 1);
        Assert.NotNull(investidor);
        Assert.NotEqual("Desconhecido", investidor.PerfilDeRisco);
    }
}
