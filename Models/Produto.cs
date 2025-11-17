public class Produto
{
    public int IdProduto { get; set; }
    public string Nome { get; set; } = String.Empty;
    public string Tipo { get; set; } = String.Empty;
    public decimal Rentabilidade { get; set; }
    public string Risco { get; set; } = String.Empty;
    
    public Produto(int idProduto, string nome, string tipo, decimal rentabilidade, string risco)
    {            
        IdProduto = idProduto;
        Nome = nome;
        Tipo = tipo;
        Rentabilidade = rentabilidade;
        Risco = risco;
    }
}