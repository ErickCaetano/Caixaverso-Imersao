# Etapa de build: compila o projeto em Release
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia todos os arquivos do repositório para dentro do container de build
COPY . .

# Restaura e publica o projeto principal
RUN dotnet restore ./DesafioPerfildeRisco/DesafioPerfildeRisco.csproj
RUN dotnet publish ./DesafioPerfildeRisco/DesafioPerfildeRisco.csproj -c Release -o /app

# Etapa de runtime: imagem leve para rodar a API
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copia a publicação gerada na etapa de build
COPY --from=build /app .

# Expõe a porta padrão (80) dentro do container
EXPOSE 80

# Inicia a API
ENTRYPOINT ["dotnet", "DesafioPerfildeRisco.dll"]
