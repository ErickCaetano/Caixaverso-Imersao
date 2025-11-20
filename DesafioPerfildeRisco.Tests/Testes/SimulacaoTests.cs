namespace DesafioPerfildeRisco.Testes;

using Xunit;
using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.Services;


public class SimulacaoTests
{
    [Fact]
    public void TestarSe_ConstrutorEValido()
    {

        // Dados para teste
        int idCliente = 10;
        Produto produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 10000m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 12;


        // Testar classe
        var simulacao = new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses);

        // Conferir resultados
        Assert.Equal(idCliente, simulacao.IdCliente);
        Assert.Equal(produto, simulacao.Produto);
        Assert.Equal(valorInvestido, simulacao.ValorInvestido);
        Assert.Equal(Math.Round(valorFinal, 2), simulacao.ValorFinal);
        Assert.Equal(Math.Round(rentabilidadeEfetiva, 2), simulacao.RentabilidadeEfetiva);
        Assert.Equal(prazoMeses, simulacao.PrazoMeses);

    }


    [Fact]
    public void TestarSe_ProdutoNulo_RetornaExcecao()
    {
        // Dados para teste
        int idCliente = 10;
        Produto produto = null!;
        decimal valorInvestido = 10000m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 12;

        // Testar e conferir 
        var ex = Assert.Throws<ArgumentNullException>(() =>
        new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses));
        Assert.Contains("O produto não pode ser nulo.", ex.Message);
    }

    [Fact]
    public void TestarSe_ValorInvestidoMaiorQueZero_RetornaExcecao()
    {
        // Dados para teste
        int idCliente = 10;
        Produto produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 0m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 12;

        // Testar e conferir 
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses));
        Assert.Contains("O valor investido deve ser maior que zero.", ex.Message);
    }

    [Fact]
    public void TestarSe_PrazoMesesMaiorQueZero_RetornaExcecao()
    {
        // Dados para teste
        int idCliente = 10;
        Produto produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 10000m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 0;

        // Testar e conferir 
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses));
        Assert.Contains("O prazo em meses deve ser maior que zero.", ex.Message);
    }

    [Fact]
    public void TestarSe_RentabilidadeEfetivaNaoNegativa_RetornaExcecao()
    {
        // Dados para teste
        int idCliente = 10;
        Produto produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 10000m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = -0.1m;
        int prazoMeses = 12;

        // Testar e conferir 
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses));
        Assert.Contains("A rentabilidade efetiva não pode ser negativa.", ex.Message);
    }

    [Fact]
    public void TestarSe_ValorFinalNaoNegativo_RetornaExcecao()
    {
        // Dados para teste
        int idCliente = 10;
        var produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 10000m;
        decimal valorFinal = -11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 12;

        // Testar e conferir 
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses));
        Assert.Contains("O valor final não pode ser negativo.", ex.Message);
    }

    [Fact]
    public void TestarSe_CadastrarIdAtribuiCorretamente()
    {
        // Dados para teste
        int idCliente = 10;
        Produto produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 10000m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 12;
        int novoId = 5;

        // Testar classe
        var simulacao = new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses);
        simulacao.CadastrarId(novoId);

        // Conferir resultados
        Assert.Equal(novoId, simulacao.IdSimulacao);
    }

    [Fact]
    public void TestarSe_CadastrarIdMenorQueUm_RetornaExcecao()
    {
        // Dados para teste
        int idCliente = 10;
        Produto produto = new Produto(1, "CDB Teste", "CDB", 0.1m, "Baixo");
        decimal valorInvestido = 10000m;
        decimal valorFinal = 11000m;
        decimal rentabilidadeEfetiva = 0.1m;
        int prazoMeses = 12;
        int novoId = -1;

        // Testar classe
        var simulacao = new Simulacao(idCliente, produto, valorInvestido, valorFinal, rentabilidadeEfetiva, prazoMeses);

        // Testar e conferir 
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        simulacao.CadastrarId(novoId));
        Assert.Contains("O ID da simulação deve ser maior ou igual a zero.", ex.Message);
    }


}