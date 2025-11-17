using System.Numerics;
using DesafioPerfilInvestidor.MockDB;
using DesafioPerfilInvestidor.Services;


public class Investidor
{
    public int IdCliente { get; private set; }
    public string PerfilDeRisco { get; private set; } = "Desconhecido";
    public int Pontuacao { get; private set; } = 0;
    public string Descricao { get; private set; } = "Investidor não classificado";
    
    public Investidor(int idCliente)
    {            
        IdCliente = idCliente;
        //AtualizarPerfil();
    }



    public void AtualizarPerfil()
    {
        Pontuacao = MotorDeRecomendacao.CalcularPontuacao(IdCliente);
        DefinirPerfilDeRisco(Pontuacao);
    }

    //     public int CalcularPontuacao()
    // {
    //     var investimentosCliente = DataBase.Simulacoes
    //     .Where(s => s.IdCliente == IdCliente)
    //     .ToList();

    //     if (investimentosCliente.Count == 0)
    //          return 0;

    //     decimal totalInvestidoRiscoBaixo = 0m;
    //     decimal totalInvestidoRiscoMedio = 0m;
    //     decimal totalInvestidoRiscoAlto = 0m;
    //     decimal somaValoresInvestidos = investimentosCliente.Sum(i => i.ValorInvestido);
        
    //     foreach (var investimento in investimentosCliente)
    //     {
    //         switch (investimento.Produto.Risco)
    //         {
    //             case "Baixo":
    //                 totalInvestidoRiscoBaixo += investimento.ValorInvestido;
    //                 break;
    //             case "Médio":
    //                 totalInvestidoRiscoMedio += investimento.ValorInvestido;
    //                 break;
    //             case "Alto":
    //                 totalInvestidoRiscoAlto +=investimento.ValorInvestido;
    //                 break;
    //         }
    //     }

    //     decimal pontuacaoponderada = (totalInvestidoRiscoBaixo / somaValoresInvestidos) * 1m +
    //                                  (totalInvestidoRiscoMedio / somaValoresInvestidos) * 45m +
    //                                  (totalInvestidoRiscoAlto / somaValoresInvestidos) * 100m;
    //     int pontuacaoFinal = (int)Math.Round(pontuacaoponderada);

    //     if (pontuacaoFinal >= 1 && pontuacaoFinal <= 100)
    //         return pontuacaoFinal;
    //     else        
    //         return 0;
    // }


    
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