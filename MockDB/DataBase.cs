using DesafioPerfilInvestidor.Models;
using Microsoft.AspNetCore.SignalR;

public static class DataBase
{
    public static List<Produto> Produtos;
    public static List<Investidor> Investidores;
    public static List<Simulacao> Simulacoes;

    static DataBase()
    {
        Produtos = new List<Produto>{
        new Produto(1, "Produto A", "CDB", 0.12m, "Baixo"),
        new Produto(2, "Produto B", "LCI", 0.10m, "Baixo"),
        new Produto(3, "Produto C", "LCA", 0.11m, "Médio"),
        new Produto(4, "Produto D", "FUNDOS", 0.15m, "Alto"),
        new Produto(5, "Produto E", "AÇÕES", 0.14m, "Alto")
    };

        Investidores = new List<Investidor>
    {
        new Investidor(1),
        new Investidor(2),
        new Investidor(3)
    };

        Simulacoes = new List<Simulacao>();
    }


    public static void AdicionarSimulacao(Simulacao simulacao)
    {
        simulacao.CadastrarId(Simulacoes.Count + 1);
        Simulacoes.Add(simulacao);
    }






}
