public class Investidor
{
    public int IdCliente { get; private set; }
    public string PerfilDeRisco { get; private set; } = "Desconhecido";
    public int Pontuacao { get; private set; } = 0;
    public string Descricao { get; private set; } = "Investidor não classificado";
    
    public Investidor(int idCliente)
    {            
        IdCliente = idCliente;
        AtualizarPerfil();
    }



    public void AtualizarPerfil()
    {
        Pontuacao = CalcularPontuacao();
        DefinirPerfilDeRisco(Pontuacao);
    }

        public int CalcularPontuacao()
    {
        return 10;
    }

//         Decimal pontuacao  = 0m;
//         int quantidade  = 0;        
//         decimal volumeInvestido  = 0m;
//         var simulacoes = DataBase.Simulacoes;

// if (!simulacoes.Any())
// {
//     Pontuacao = 0;
//     PerfilDeRisco = "Desconhecido";
//     Descricao = "Investidor não classificado";
//     return;
// }  
        
// foreach (var simulacao in simulacoes.Where(s => s.IdCliente == IdCliente))
// {
//     quantidade++;
//     volumeInvestido += simulacao.ValorInvestido;

//     switch (simulacao.Produto.Risco)
//     {
//         case "Baixo":
//             pontuacao += 1 * simulacao.ValorInvestido;
//             break;
//         case "Médio":
//             pontuacao += 45 * simulacao.ValorInvestido;
//             break;
//         case "Alto":
//             pontuacao += 100 * simulacao.ValorInvestido;
//             break;
//     }
// }

//         if (quantidade > 0 && volumeInvestido > 0)
//         {
//             Pontuacao = (int)(pontuacao / (volumeInvestido * quantidade));
//             DefinirPerfilDeRisco(Pontuacao);
//         }
//         else
//         {
//             Pontuacao = 0;
//             PerfilDeRisco = "Desconhecido";
//             Descricao = "Investidor não classificado";
//         }
        




//     }
    
    private void DefinirPerfilDeRisco(int pontuacao)
    {
        if (pontuacao >= 1 && pontuacao <= 30)
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