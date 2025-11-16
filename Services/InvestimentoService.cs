using DesafioPerfilInvestidor.DTOs;
using DesafioPerfilInvestidor.Models;
namespace DesafioPerfilInvestidor.Services;

public class InvestimentoService : IEmprestimoServico
{
    public Simulacao SimularInvestimento(SimulacaoRequest DadosSimulacao)
    {

        //Simula a busca do produto no banco de dados

        var produtoTeste = DataBase.Produtos.FirstOrDefault(p => p.Tipo == DadosSimulacao.TipoProduto);



        //Calculo do investimento
        var txJurosMensal = Math.Pow(1.0 + (double)produtoTeste.Rentabilidade / 100.0, 1.0 / 12.0) - 1.0;
        decimal valorFinal = DadosSimulacao.ValorInvestido * (decimal)Math.Pow(1.0 + txJurosMensal, DadosSimulacao.PrazoMeses);
        decimal rentabilidadeEfetiva = produtoTeste.Rentabilidade;

        var resultado = new Simulacao(DadosSimulacao.IdCliente,produtoTeste, DadosSimulacao.ValorInvestido, valorFinal, rentabilidadeEfetiva, DadosSimulacao.PrazoMeses); 

        DataBase.AdicionarSimulacao(resultado);

        //Cria o objeto Simulação para retorno e armazenamento
        return resultado;
         
    }

public List<ProdutoeDiaResponse> produtoeDias()
    {
        var lista = new List<ProdutoeDiaResponse>();

// public ProdutoeDiaResponse(string produto, string data, int quantidadeDeSimulacoes, decimal mediaValorFinal)



        foreach (var sim in DataBase.Simulacoes)
        {

        }
        return lista;
    }




}