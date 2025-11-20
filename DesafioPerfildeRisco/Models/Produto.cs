namespace DesafioPerfildeRisco.Models;
public class Produto
{
    public int IdProduto { get; set; }
    public string Nome { get; set; } = String.Empty;
    public string Tipo { get; set; } = String.Empty;
    public decimal Rentabilidade { get; set; }
    public string Risco { get; set; } = String.Empty;
    
    public Produto(int idProduto, string nome, string tipo, decimal rentabilidade, string risco)
    { 
        // Validações
        if (idProduto<1)
            throw new ArgumentOutOfRangeException(nameof(idProduto), "O ID do produto deve ser maior que 1.");
     
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));

        if (string.IsNullOrWhiteSpace(tipo))
            throw new ArgumentException("Tipo de produto não pode ser vazio", nameof(tipo));
            
        if (rentabilidade < 0m)
            throw new ArgumentOutOfRangeException(nameof(rentabilidade), "A rentabilidade não pode ser negativa.");      
            
        if (string.IsNullOrWhiteSpace(risco))
            throw new ArgumentException("O tipo de risco não pode ser vazio", nameof(risco));

        if (risco != "Baixo" && risco != "Médio" && risco != "Alto")
            throw new ArgumentException("O tipo de risco precisa ser Baixo, Médio ou Alto.", nameof(risco));

        // Atribuições
        IdProduto = idProduto;
        Nome = nome;
        Tipo = tipo;
        Rentabilidade = rentabilidade;
        Risco = risco;
    }
}