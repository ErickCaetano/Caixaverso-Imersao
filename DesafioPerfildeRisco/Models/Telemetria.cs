namespace DesafioPerfildeRisco.Models;

public class Telemetria
{
    public int Id { get; set; }
    public string NomeServico { get; set; } = default!;
    public long DuracaoMs { get; set; }
    public DateTime DataHora { get; set; } = DateTime.UtcNow;
}