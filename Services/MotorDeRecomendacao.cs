namespace DesafioPerfilInvestidor.Services;

using DesafioPerfilInvestidor.MockDB;



public static class MotorDeRecomendacao
{
    public static int CalcularPontuacao(int idCliente)
    {
        // return 10;
        var investimentosCliente = DataBase.Simulacoes
        .Where(s => s.IdCliente == idCliente)
        .ToList();

        if (investimentosCliente.Count == 0)
            return 0;

        decimal totalInvestidoRiscoBaixo = 0m;
        decimal totalInvestidoRiscoMedio = 0m;
        decimal totalInvestidoRiscoAlto = 0m;
        decimal somaValoresInvestidos = investimentosCliente.Sum(i => i.ValorInvestido);

        foreach (var investimento in investimentosCliente)
        {
            switch (investimento.Produto.Risco)
            {
                case "Baixo":
                    totalInvestidoRiscoBaixo += investimento.ValorInvestido;
                    break;
                case "Médio":
                    totalInvestidoRiscoMedio += investimento.ValorInvestido;
                    break;
                case "Alto":
                    totalInvestidoRiscoAlto += investimento.ValorInvestido;
                    break;
            }
        }

        decimal pontuacaoponderada = (totalInvestidoRiscoBaixo / somaValoresInvestidos) * 1m +
                                     (totalInvestidoRiscoMedio / somaValoresInvestidos) * 45m +
                                     (totalInvestidoRiscoAlto / somaValoresInvestidos) * 100m;
        int pontuacaoFinal = (int)Math.Round(pontuacaoponderada);

        if (pontuacaoFinal >= 1 && pontuacaoFinal <= 100)
            return pontuacaoFinal;
        else
            return 0;

    }
}
