namespace DesafioPerfildeRisco.Testes;

using DesafioPerfildeRisco.Models;
using DesafioPerfildeRisco.Services;

using Xunit;



public class InvestidorTests
{
    [Fact]
    public void TestarSe_ConstrutorEValido()
    {
        // Dados para teste
        int IdCliente = 20;
        string PerfilDeRisco = "Desconhecido";
        int Pontuacao = 0;
        string Descricao = "Investidor não classificado";


        // Testar classe
        var investidor = new Investidor(IdCliente);
        investidor.PerfilDeRisco = PerfilDeRisco;
        investidor.Pontuacao = Pontuacao;
        investidor.Descricao = Descricao;

        // Conferir resultados
        Assert.Equal(IdCliente, investidor.IdCliente);
        Assert.Equal(PerfilDeRisco, investidor.PerfilDeRisco);
        Assert.Equal(Pontuacao, investidor.Pontuacao);
        Assert.Equal(Descricao, investidor.Descricao);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TestarSe_IdClienteMaiorQueUm_RetornaExcecao(int idCliente)
    {
        // Testar e conferir 
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        new Investidor(idCliente));
        Assert.Contains("O ID do cliente deve ser maior que 1.", ex.Message);
    }


 [Theory]
[InlineData(1, "Conservador")]
[InlineData(30, "Conservador")]
[InlineData(31, "Moderado")]
[InlineData(60, "Moderado")]
[InlineData(61, "Agressivo")]
[InlineData(100, "Agressivo")]
[InlineData(0, "Desconhecido")] // 0 é tratado como válido
public void TestarSe_PerfilDeRiscoValido_AtualizaCorretamente(int pontuacao, string perfilEsperado)
{
    var investidor = new Investidor(20);
    investidor.AtualizarPerfil(pontuacao);

    Assert.Equal(perfilEsperado, investidor.PerfilDeRisco);
}

[Theory]
[InlineData(-1)]
[InlineData(101)]
public void TestarSe_PerfilDeRiscoInvalido_RetornaExcecao(int pontuacao)
{
    var investidor = new Investidor(20);

    var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
        investidor.AtualizarPerfil(pontuacao));

    Assert.Contains("Pontuação inválida para calcular perfil de risco.", ex.Message);
}

}
