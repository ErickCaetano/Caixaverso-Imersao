
using DesafioPerfilInvestidor.Models;
using DesafioPerfilInvestidor.Services;
using Microsoft.EntityFrameworkCore;
using DesafioPerfilInvestidor.MockDB;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Desafio BackEnd C#", Version = "v1" });
  
c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Description = "Insira apenas o token JWT abaixo sem aspas. O Swagger adiciona 'Bearer' automaticamente.",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http, 
    Scheme = "bearer",
    BearerFormat = "JWT"
});

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=./Data/db.sqlite"));

builder.Services.AddScoped<IInvestimentoServico, InvestimentoService>();
builder.Services.AddScoped<MotorDeRecomendacao>();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("AdoreiASenhaDele-EstaAprovado!!!!!!!!!!!!!!!!!!!"))
        };
    });

    builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

