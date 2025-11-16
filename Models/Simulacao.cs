using System.Data;
namespace DesafioPerfilInvestidor.Models;

public class Simulacao
{
    public int IdSimulacao { get; private set; }
    public int IdCliente { get; private set; }
    public Produto Produto { get; private set; }
    public decimal ValorInvestido { get; private set; }
    public decimal ValorFinal { get; private set; }
    public int PrazoMeses { get; private set; }
    public DateTime DataSimulacao { get; private set; }
    private decimal RentabilidadeEfetiva;

    public Simulacao(int idCliente,Produto produto, decimal valorInvestido, decimal valorFinal, decimal rentabilidadeEfetiva, int prazoMeses)
    {
        IdSimulacao = 0; // Será atribuído pelo banco de dados
        IdCliente = idCliente;
        Produto = produto;
        ValorInvestido = valorInvestido;
        ValorFinal = Math.Round(valorFinal, 2);
        RentabilidadeEfetiva = Math.Round(rentabilidadeEfetiva, 2);
        PrazoMeses = prazoMeses;
        DataSimulacao = DateTime.Now;
    }

    public decimal VerRentabilidade()
    {
        return RentabilidadeEfetiva;
    }

        public void CadastrarId(int id)
    {
        IdSimulacao = id;
    }       
    
}