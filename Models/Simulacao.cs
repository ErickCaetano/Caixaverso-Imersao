using System.Data;
namespace DesafioPerfilInvestidor.Models;

public class Simulacao
{
    public int IdSimulacao { get; set; }
    public int IdCliente { get; set; }
    public  Produto Produto { get; set; } = null!;
    public decimal ValorInvestido { get; set; }
    public decimal ValorFinal { get; set; }
    public int PrazoMeses { get; set; }
    public DateTime DataSimulacao { get; set; }
    public decimal RentabilidadeEfetiva;

    public Simulacao(){}

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