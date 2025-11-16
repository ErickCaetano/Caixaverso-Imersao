using DesafioPerfilInvestidor.DTOs;
using DesafioPerfilInvestidor.Models;
namespace DesafioPerfilInvestidor.Services;

public class InvestimentoService : IInvestimentoServico
{
    public Simulacao SimularInvestimento(SimulacaoRequest DadosSimulacao)
    {

        //Simula a busca do produto no banco de dados

        Produto? produtoTeste = DataBase.Produtos.FirstOrDefault(p => p.Tipo == DadosSimulacao.TipoProduto);

        if (produtoTeste == null)
        {
            throw new ArgumentException("Produto de investimento não encontrado para o tipo especificado.");
        }

        //Calculo do investimento
        var txJurosMensal = Math.Pow(1.0 + (double)produtoTeste.Rentabilidade / 100.0, 1.0 / 12.0) - 1.0;
        decimal valorFinal = DadosSimulacao.ValorInvestido * (decimal)Math.Pow(1.0 + txJurosMensal, DadosSimulacao.PrazoMeses);
        decimal rentabilidadeEfetiva = produtoTeste.Rentabilidade;

        var resultado = new Simulacao(DadosSimulacao.IdCliente,produtoTeste, DadosSimulacao.ValorInvestido, valorFinal, rentabilidadeEfetiva, DadosSimulacao.PrazoMeses); 

        DataBase.AdicionarSimulacao(resultado);

        var novoInvestidor = new Investidor (DadosSimulacao.IdCliente);

        if (!DataBase.Investidores.Any(i => i.IdCliente == DadosSimulacao.IdCliente))
        {
            DataBase.Investidores.Add(novoInvestidor);
        }
        else
        {
            var investidorExistente = DataBase.Investidores.First(i => i.IdCliente == DadosSimulacao.IdCliente);
            investidorExistente.AtualizarPerfil();
        }


        //Cria o objeto Simulação para retorno e armazenamento
        return resultado;
         
    }


}