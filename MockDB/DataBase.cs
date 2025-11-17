// using DesafioPerfilInvestidor.Models;
// using Microsoft.AspNetCore.SignalR;
// namespace DesafioPerfilInvestidor.MockDB;

// public static class DataBase
// {
//     public static List<Produto> Produtos;
    
//     public static List<Simulacao> Simulacoes;
//     public static List<Investidor> Investidores;

//     static DataBase()
//     {
//         Produtos = new List<Produto>{
//         new Produto(1, "Produto A", "CDB", 0.12m, "Baixo"),
//         new Produto(2, "Produto B", "LCI", 0.10m, "Baixo"),
//         new Produto(3, "Produto C", "LCA", 0.11m, "Médio"),
//         new Produto(4, "Produto D", "FUNDOS", 0.15m, "Alto"),
//         new Produto(5, "Produto E", "AÇÕES", 0.14m, "Alto")
//     };

//         Simulacoes = new List<Simulacao>();

//         Investidores = new List<Investidor>
//     {
//         new Investidor(1),
//         new Investidor(2),
//         new Investidor(3)
//     };


//     }


//     public static void AdicionarSimulacao(Simulacao simulacao)
//     {
//         simulacao.CadastrarId(Simulacoes.Count + 1);
//         Simulacoes.Add(simulacao);
//     }

// }
