using DesafioPerfilInvestidor.MockDB;
using DesafioPerfilInvestidor.Models;

public class MiddlewareTelemetria
{
    private readonly RequestDelegate _next;

    public MiddlewareTelemetria(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context, AppDbContext db)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await _next(context);
        sw.Stop();

        var endpoint = context.GetEndpoint();
        var actionName = endpoint?.Metadata
            .GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()?.ActionName
            ?? context.Request.Path.Value ?? "unknown";

        db.Telemetrias.Add(new Telemetria
        {
            NomeServico = actionName,
            DuracaoMs = sw.ElapsedMilliseconds,
            DataHora = DateTime.UtcNow
        });
        await db.SaveChangesAsync();
    }
}