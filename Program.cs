using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using DesafioPerfilInvestidor.Models;
using DesafioPerfilInvestidor.Services;
using Microsoft.EntityFrameworkCore;
using DesafioPerfilInvestidor.MockDB;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=./Data/db.sqlite"));

builder.Services.AddScoped<IInvestimentoServico, InvestimentoService>();
builder.Services.AddScoped<MotorDeRecomendacao>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DesafioPerfilInvestidor.MockDB.AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Produtos.Any())
    {
        db.Produtos.AddRange(new[]
        {
            new Produto(1, "Produto A", "CDB", 0.12m, "Baixo"),
            new Produto(2, "Produto B", "LCI", 0.10m, "Baixo"),
            new Produto(3, "Produto C", "LCA", 0.11m, "Médio"),
            new Produto(4, "Produto D", "FUNDOS", 0.15m, "Alto"),
            new Produto(5, "Produto E", "AÇÕES", 0.14m, "Alto")
        });
        db.SaveChanges();
    }

    if (!db.Investidores.Any())
    {
        db.Investidores.AddRange(new[]
        {
            new Investidor(1),
            new Investidor(2),
            new Investidor(3)
        });
        db.SaveChanges();
    }
}



app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

