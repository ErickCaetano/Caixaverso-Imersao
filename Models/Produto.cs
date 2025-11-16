public class Produto
{
    public int IdProduto { get; private set; }
    public string Nome { get; private set; } = String.Empty;
    public string Tipo { get; private set; } = String.Empty;
    public decimal Rentabilidade { get; private set; }
    public string Risco { get; private set; } = String.Empty;
    
    public Produto(int idProduto, string nome, string tipo, decimal rentabilidade, string risco)
    {            
        IdProduto = idProduto;
        Nome = nome;
        Tipo = tipo;
        Rentabilidade = rentabilidade;
        Risco = risco;
    }
}