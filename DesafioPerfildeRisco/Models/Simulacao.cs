namespace DesafioPerfildeRisco.Models;

public class Simulacao
{
    public int IdSimulacao { get; set; }
    public int IdCliente { get; set; }
    public Produto Produto { get; set; } = null!;
    public decimal ValorInvestido { get; set; }
    public decimal ValorFinal { get; set; }
    public int PrazoMeses { get; set; }
    public DateTime DataSimulacao { get; set; }
    public decimal RentabilidadeEfetiva { get; set; }

    public Simulacao() { }

    public Simulacao(int idCliente, Produto produto, decimal valorInvestido, decimal valorFinal, decimal rentabilidadeEfetiva, int prazoMeses)
    { 
        if (produto == null)
            throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");
        if (valorInvestido <= 0m)
            throw new ArgumentOutOfRangeException(nameof(valorInvestido), "O valor investido deve ser maior que zero.");
        if (prazoMeses < 1)
            throw new ArgumentOutOfRangeException(nameof(prazoMeses), "O prazo em meses deve ser maior que zero.");
        if (rentabilidadeEfetiva < 0m)
            throw new ArgumentOutOfRangeException(nameof(rentabilidadeEfetiva), "A rentabilidade efetiva não pode ser negativa.");
        if (valorFinal < 0m)
            throw new ArgumentOutOfRangeException(nameof(valorFinal), "O valor final não pode ser negativo.");

        IdSimulacao = 0; // Será atribuído pelo banco de dados
        IdCliente = idCliente;
        Produto = produto;
        ValorInvestido = valorInvestido;
        ValorFinal = Math.Round(valorFinal, 2);
        RentabilidadeEfetiva = Math.Round(rentabilidadeEfetiva, 2);
        PrazoMeses = prazoMeses;
        DataSimulacao = DateTime.Now;
    }


    public void CadastrarId(int id)
    {
        if (id < 0)
            throw new ArgumentOutOfRangeException(nameof(id), "O ID da simulação deve ser maior ou igual a zero.");
        IdSimulacao = id;
    }

}