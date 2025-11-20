using Xunit;
using Microsoft.EntityFrameworkCore;
using DesafioPerfildeRisco.DbContext;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.Services;
using System;
using System.Threading.Tasks;

public class MotorDeRecomendacaoTests
{
    private readonly AppDbContext _db;
    private readonly MotorDeRecomendacao _motor;

    public MotorDeRecomendacaoTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _db = new AppDbContext(options);
        _motor = new MotorDeRecomendacao(_db);
    }

    [Fact]
    public async Task CalcularPontuacao_QuandoNaoHaInvestimentos_DeveRetornarZero()
    {
        var pontuacao = await _motor.CalcularPontuacao(1);
        Assert.Equal(0, pontuacao);
    }

    [Fact]
    public async Task CalcularPontuacao_QuandoSomenteRiscoBaixo_DeveRetornar1()
    {

        _db.Simulacoes.Add(new Simulacao(1, new Produto(1, "CDB", "CDB", 0.12m, "Baixo"), 1000, 1100, 0.12m, 12));
        await _db.SaveChangesAsync();

        var pontuacao = await _motor.CalcularPontuacao(1);
        Assert.Equal(1, pontuacao);
    }

    [Fact]
    public async Task CalcularPontuacao_QuandoSomenteRiscoMedio_DeveRetornar45()
    {
        _db.Simulacoes.Add(new Simulacao(2, new Produto(2, "LCI", "LCI", 0.15m, "Médio"), 1000, 1150, 0.15m, 12));
        await _db.SaveChangesAsync();

        var pontuacao = await _motor.CalcularPontuacao(2);
        Assert.Equal(45, pontuacao);
    }

    [Fact]
    public async Task CalcularPontuacao_QuandoSomenteRiscoAlto_DeveRetornar100()
    {
        _db.Simulacoes.Add(new Simulacao(3, new Produto(3, "Ações", "Ações", 0.20m, "Alto"), 1000, 1200, 0.20m, 12));
        await _db.SaveChangesAsync();

        var pontuacao = await _motor.CalcularPontuacao(3);
        Assert.Equal(100, pontuacao);
    }

    [Fact]
    public async Task CalcularPontuacao_QuandoMisturaDeRiscos_DeveCalcularCorretamente()
    {

        _db.Simulacoes.Add(new Simulacao(4,new Produto(4, "CDB", "CDB", 0.12m, "Baixo"), 1000, 1100, 0.12m, 12));
        _db.Simulacoes.Add(new Simulacao(4,new Produto(5, "LCI", "LCI", 0.15m, "Médio"), 1000, 1150, 0.15m, 12));
        _db.Simulacoes.Add(new Simulacao(4,new Produto(6, "Ações", "Ações", 0.20m, "Alto"), 1000, 1200, 0.20m, 12));
        await _db.SaveChangesAsync();

        var pontuacao = await _motor.CalcularPontuacao(4);

        // Deve ser a média ponderada: (1 + 45 + 100) / 3 ≈ 49
        Assert.InRange(pontuacao, 45, 55);
    }

    [Fact]
    public async Task CalcularPontuacao_DeveIgnorarInvestimentosAntigos()
    {
        var produto = new Produto(7, "CDB", "CDB", 0.12m, "Baixo");

        // Simulação antiga (mais de 24 meses atrás)
        var simulacaoAntiga = new Simulacao(7, produto, 1000, 1100, 0.12m, 12);
        simulacaoAntiga.DataSimulacao = DateTime.Now.AddYears(-3);
        _db.Simulacoes.Add(simulacaoAntiga);

        await _db.SaveChangesAsync();

        var pontuacao = await _motor.CalcularPontuacao(7);
        Assert.Equal(0, pontuacao);
    }
}
