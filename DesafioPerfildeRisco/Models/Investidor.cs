namespace DesafioPerfildeRisco.Models;
using System.Numerics;
using DesafioPerfildeRisco.Services;


public class Investidor
{
    public int IdCliente { get; set; }
    public string PerfilDeRisco { get; set; } = "Desconhecido";
    public int Pontuacao { get; set; } = 0;
    public string Descricao { get; set; } = "Investidor não classificado";
    
    public Investidor(int idCliente)
    {            
        // Validações
        if (idCliente<1)
            throw new ArgumentOutOfRangeException(nameof(idCliente), "O ID do cliente deve ser maior que 1.");


        IdCliente = idCliente;
    }

 


    public void AtualizarPerfil(int pontuacao)
    {
        DefinirPerfilDeRisco(pontuacao);        
        Pontuacao = pontuacao;
    }

    
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
            else if (pontuacao == 0)
            {
                PerfilDeRisco = "Desconhecido";
                Descricao = "Investidor não classificado";
            }
        else
                    {
                PerfilDeRisco = "Desconhecido";
                Descricao = "Investidor não classificado";            
                throw new ArgumentOutOfRangeException(nameof(pontuacao), "Pontuação inválida para calcular perfil de risco.");
                    }
    }
    
}