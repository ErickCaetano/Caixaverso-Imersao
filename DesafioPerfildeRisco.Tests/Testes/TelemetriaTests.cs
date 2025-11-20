namespace DesafioPerfildeRisco.Tests;
using Xunit;
using DesafioPerfildeRisco.Models;


public class TelemetriaTests
{

    [Fact]
    public void Propriedades_PodemSerDefinidasCorretamente()
    {
        var telemetria = new Telemetria
        {
            Id = 1,
            NomeServico = "TesteServico",
            DuracaoMs = 123,
            DataHora = new DateTime(2025, 11, 19, 21, 30, 0, DateTimeKind.Utc)
        };

        Assert.Equal(1, telemetria.Id);
        Assert.Equal("TesteServico", telemetria.NomeServico);
        Assert.Equal(123, telemetria.DuracaoMs);
        Assert.Equal(new DateTime(2025, 11, 19, 21, 30, 0, DateTimeKind.Utc), telemetria.DataHora);
    }
}
