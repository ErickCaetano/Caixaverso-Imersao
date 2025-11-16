public class Investidor
{
    public int IdCliente { get; private set; }
    public string PerfilDeRisco { get; private set; } = "Desconhecido";
    public int Pontuacao { get; private set; }
    public string Descricao { get; private set; } = "Investidor não classificado";
    
    public Investidor(int idCliente, int pontuacao)
    {            
        IdCliente = idCliente;
        AtualizarPontuacao(pontuacao);
    }

    public void AtualizarPontuacao(int novaPontuacao)
    {
        DefinirPerfilDeRisco(novaPontuacao);
        Pontuacao = novaPontuacao;
    }
    
    private void DefinirPerfilDeRisco(int pontuacao)
    {
        if (pontuacao >= 0 && pontuacao <= 30)
        {
            PerfilDeRisco = "Conservador";
            Descricao = "Perfil com baixa tolerância ao risco, prefere segurança e estabilidade em seus investimentos.";

        }
        else if (pontuacao >= 31 && pontuacao <= 60)
        {
            PerfilDeRisco =  "Moderado";            
            Descricao = "Perfil que busca equilíbrio entre risco e retorno, aceitando alguma volatilidade para obter melhores ganhos.";
        }
        else if (pontuacao >= 61 && pontuacao <= 100)
        {
            PerfilDeRisco =  "Agressivo";
            Descricao = "Perfil com alta tolerância ao risco, disposto a enfrentar volatilidade significativa em busca de maiores retornos.";
            }
        else
            throw new ArgumentOutOfRangeException(nameof(pontuacao), "Pontuação inválida para calcular perfil de risco.");
    }
    
}