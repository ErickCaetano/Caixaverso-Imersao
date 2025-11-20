namespace DesafioPerfildeRisco.Services;

using DesafioPerfildeRisco.DTOs;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.DbContext;
using Microsoft.EntityFrameworkCore;

public class InvestimentoService : IInvestimentoServico
{
    private readonly AppDbContext _db;
    private readonly MotorDeRecomendacao _motor;

    public InvestimentoService(AppDbContext db, MotorDeRecomendacao motor)
    {
        _db = db;
        _motor = motor;
    }
    public async Task<Simulacao> SimularInvestimento(SimulacaoRequest DadosSimulacao)
    {



        if (DadosSimulacao.IdCliente <= 0 ||
    string.IsNullOrWhiteSpace(DadosSimulacao.TipoProduto) ||
    DadosSimulacao.ValorInvestido <= 0 ||
    DadosSimulacao.PrazoMeses <= 0)
        {
            throw new ArgumentException("Dados de entrada inválidos. Verifique os parâmetros da simulação.");
        }

        var tipoNormalizado = DadosSimulacao.TipoProduto.Trim().ToUpperInvariant();

        // Verifica se existe no banco
        var produto = _db.Produtos.FirstOrDefault(p => p.Tipo.ToUpper() == tipoNormalizado);

        if (produto == null)
        {
            throw new ArgumentException($"Produto de investimento '{DadosSimulacao.TipoProduto}' não encontrado no banco de dados.");
        }

        //Buscar o produto de investimento baseado no tipo fornecido
        var produtoTeste = await _db.Produtos
        .FirstOrDefaultAsync(p => p.Tipo == DadosSimulacao.TipoProduto);


        if (produtoTeste == null)
        {
            throw new ArgumentException("Produto de investimento não encontrado para o tipo especificado.");
        }



        //Calculo do investimento
        var txJurosMensal = Math.Pow(1.0 + (double)produtoTeste.Rentabilidade, 1.0 / 12.0) - 1.0;
        decimal valorFinal = DadosSimulacao.ValorInvestido * (decimal)Math.Pow(1.0 + txJurosMensal, DadosSimulacao.PrazoMeses);
        decimal rentabilidadeEfetiva = produtoTeste.Rentabilidade;

        var resultado = new Simulacao(DadosSimulacao.IdCliente, produtoTeste, DadosSimulacao.ValorInvestido, valorFinal, rentabilidadeEfetiva, DadosSimulacao.PrazoMeses);

        //Armazenar a simulação no banco de dados
        _db.Simulacoes.Add(resultado);
        await _db.SaveChangesAsync();

        var investidorExistente = await _db.Investidores
        .FirstOrDefaultAsync(i => i.IdCliente == DadosSimulacao.IdCliente);

        // var novoInvestidor = new Investidor (DadosSimulacao.IdCliente);

        if (investidorExistente == null)
        {
            var novoInvestidor = new Investidor(DadosSimulacao.IdCliente);
            _db.Investidores.Add(novoInvestidor);
        }
        else
        {
            int pontuacao = await _motor.CalcularPontuacao(DadosSimulacao.IdCliente);
            investidorExistente.AtualizarPerfil(pontuacao);
            _db.Investidores.Update(investidorExistente);
        }

        await _db.SaveChangesAsync();

        return resultado;



    }
}