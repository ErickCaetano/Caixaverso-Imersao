# Desafio Perfil de Risco

Projeto realizado para cumprir o desafio Caixaverso para o cargo de assistente pleno em BackEnd  C#, entre as datas 14/11/2025 a 21/11/2025.


## Descrição:

O projeto é uma API dividida em models, services, controllers, data e dto.


## Tecnologias:
- .NET 9
- Entity Framework Core
- SQLite
- JWT
- Docker & Docker Compose
- xUnit (testes unitários)


## Instruções de execução:

### Localmente
```bash
dotnet restore
dotnet run --project DesafioPerfildeRisco
```

### Via Docker
```bash
docker-compose up --build
```

**API disponível em: http://localhost:5260/swagger/index.html**

## Auntenticação
Para utilizar a API é necessário autenticar usuario e senha com Antenticação JWT.
Utilizando o Endpoint Auth, já configurado com a senha correta para facilitar os testes.
Copiar o Token gerado (sem aspas) e acionar o botão Authorize no Swwager.
Todos os Endpoints Exceto Auth só podem ser acessados com autorização.

## Endpoints:

### Lista Resumida:

- **POST** /api/Auth/login

- **POST** /api/Investimento/simular-investimento
- **GET** /api/Investimento/simulacoes
- **GET** /api/Investimento/simulacoes/por-produto-dia
- **GET** /api/Investimento/telemetria
- **GET** /api/Investimento/perfil-risco/{clienteId}
- **GET** /api/Investimento/produtos-recomendados/{perfil}
- **GET** /api/Investimento/investimentos/{clienteId}



### Detalhamento:

#### **POST** /api/Auth/login

Recebe os dados de usuário e senha e, caso corretos, retorna um token de autenticação.
Por default preenchido com "admin" e "123", basta utilizar.

Exemplo
Request:

```json
{
  "username": "admin",
  "password": "123"
}
```
Response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImV4cCI6MTc2MzcyODU2OCwiaXNzIjoibWV1U2lzdGVtYSIsImF1ZCI6Im1ldVNpc3RlbWEifQ.XsTvIBpXZ4FSwxthowAgqESngt07v6AT7HztorQMmDE"
}
```


#### **POST** /api/Investimento/simular-investimento

Realiza a simulação de investimentos baseado nos dados entregues na request.
Exemplo:

Request:

```json
{
  "idCliente": 1,
  "valorInvestido": 10000,
  "prazoMeses": 12,
  "tipoProduto": "CDB"
}
```
Response:

```json
{
  "produtoValidado": {
    "id": 1,
    "nome": "Produto A",
    "tipo": "CDB",
    "rentabilidade": 0.12,
    "risco": "Baixo"
  },
  "resultadoSimulacao": {
    "valorFinal": 11200,
    "rentabilidadeEfetiva": 0.12,
    "prazoMeses": 12
  },
  "dataSimulacao": "2025-11-20T16:30:47Z"
}
```

#### **GET** /api/Investimento/simulacoes

Realiza a consulta de todas as simulações efetuadas.
Exemplo:

Response:

```json
[
  {
    "id": 1,
    "clienteId": 1,
    "produto": "Produto A",
    "valorInvestido": 10000,
    "valorFinal": 11200,
    "prazoMeses": 12,
    "dataSimulacao": "2025-11-18T20:00:25Z"
  },
  {
    "id": 2,
    "clienteId": 1,
    "produto": "Produto A",
    "valorInvestido": 12000,
    "valorFinal": 13440,
    "prazoMeses": 12,
    "dataSimulacao": "2025-11-18T20:00:33Z"
  }
]
```

#### **GET** /api/Investimento/simulacoes/por-produto-dia

Lista a quantidade de simulações por tipo de produto e data da simulação.
Exemplo:

Response:

```json
[
  {
    "produto": "Produto A",
    "data": "2025-11-18",
    "quantidadeSimulacoes": 2,
    "mediaValorFinal": 12320
  },
  {
    "produto": "Produto A",
    "data": "2025-11-19",
    "quantidadeSimulacoes": 1,
    "mediaValorFinal": 11200
  },
  {
    "produto": "Produto B",
    "data": "2025-11-19",
    "quantidadeSimulacoes": 1,
    "mediaValorFinal": 15200
  }
]
```


#### **GET** /api/Investimento/telemetria

Retorna dos dados de telemetria com volumes e tempo de resposta para cada serviço.
Exemplo:

Response:

```json
{
  "servicos": [
    {
      "nome": "ListarHistorico",
      "quantidadeChamadas": 9,
      "mediaTempoRespostaMs": 63
    },
    {
      "nome": "ListarInvestimentosPorCliente",
      "quantidadeChamadas": 3,
      "mediaTempoRespostaMs": 16
    },
    {
      "nome": "ListarProdutoPorDia",
      "quantidadeChamadas": 2,
      "mediaTempoRespostaMs": 10
    },
    {
      "nome": "Login",
      "quantidadeChamadas": 5,
      "mediaTempoRespostaMs": 74
    },
    {
      "nome": "PerfilDeRisco",
      "quantidadeChamadas": 2,
      "mediaTempoRespostaMs": 5
    },
    {
      "nome": "RecomendacaoPerfil",
      "quantidadeChamadas": 2,
      "mediaTempoRespostaMs": 10
    },
    {
      "nome": "Simular",
      "quantidadeChamadas": 6,
      "mediaTempoRespostaMs": 113
    },
    {
      "nome": "VerTelemetria",
      "quantidadeChamadas": 4,
      "mediaTempoRespostaMs": 63
    }
  ],
  "periodo": {
    "inicio": "2025-11-18",
    "fim": "2025-11-20"
  }
}
```

#### **GET** /api/Investimento/perfil-risco/{clienteId}

Retorna o perfil de risco de determinado investidor.
Exemplo:

Response:

```json
{
  "idCliente": 1,
  "perfilDeRisco": "Conservador",
  "pontuacao": 1,
  "descricao": "Perfil com baixa tolerância ao risco, prefere segurança e estabilidade em seus investimentos."
}
```

#### **GET** /api/Investimento/produtos-recomendados/{perfil}

Retorna os produtos recomendados para determinado perfil de risco.
Exemplo:

Input:

```json
{"Conservador"}
```

Response:

```json
[
  {
    "idProduto": 1,
    "nome": "Produto A",
    "tipo": "CDB",
    "rentabilidade": 0.12,
    "risco": "Baixo"
  },
  {
    "idProduto": 2,
    "nome": "Produto B",
    "tipo": "LCI",
    "rentabilidade": 0.1,
    "risco": "Baixo"
  }
]
```

#### **GET** /api/Investimento/investimentos/{clienteId}

Retorna os investimentos (1*) de determinado investidor.
Exemplo:

Input:

```json
{1}
```
Response:

```json
[
  {
    "id": 1,
    "tipo": "CDB",
    "valor": 15000,
    "rentabilidade": 0.12,
    "data": "2025-11-20"
  },
  {
    "id": 2,
    "tipo": "FUNDOS",
    "valor": 40000,
    "rentabilidade": 0.15,
    "data": "2025-11-20"
  },
  {
    "id": 3,
    "tipo": "CDB",
    "valor": 10000,
    "rentabilidade": 0.12,
    "data": "2025-11-20"
  },
  {
    "id": 4,
    "tipo": "AÇÕES",
    "valor": 10000,
    "rentabilidade": 0.14,
    "data": "2025-11-20"
  }
]
```

## Testes

Os testes unitários cobrem:
- Models
- Controllers
- Services

Para rodar os testes(no diretorio principal):
```bash
dotnet test 
```

## Motor de recomendação

Calcula a pontuação que representa a tolerância ao risco do investidor. 

Leva em consideração:
- Volume de cada investimento
- Tipo de cada investimento
- Data de cada investimento

A partir da pontuação é atribuido, no model investidor, um perfil de risco entre "Conservador", "Moderado" e "Agressivo".

## Versionamento
Foi utilizado o git para versionamento, e o relatorio de commits pode ser encontrado no diretorio principal.

## Github
Repositório público: [https://github.com/ErickCaetano/Caixaverso-Imersao]

## Autor
Projeto realizado de acordo com os requisitos por:

Erick Caetano de Oliveira Bastos

VIVAR AG3390 - Sarapuí, C158571

## Observações
(1*)Nota: devido ao escopo limitado do projeto, as simulações foram consideradas como investimentos reais para cálculo da pontuação e listagem de histórico.