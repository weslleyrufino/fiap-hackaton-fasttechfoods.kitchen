# FIAP-Fase2-TechChallenge
FIAP PÓS TECH
Fase 2 - Arquitetura de Sistemas .NET. 

Projeto onde consiste em criar uma agenda de contatos usando .NET 8.
Arquitetura escolhida: Arquitetura em Camadas.
Tecnologias utilizadas:
  - .NET 8
  - C#
  - Entity Framework Core
  - Sql Server
  - Code First e Migrations
  - XUnit
  - Bogus
  - Moq

## Nessa segunda fase será criado:

CI Pipeline:

- Build: compilar o projeto para garantir que não há erros de compilação.
- Testes Unitários: executar testes unitários para garantir que as 
funcionalidades estão trabalhando conforme o esperado.
- Testes de Integração: executar testes de integração para validar o 
funcionamento correto entre os componentes do sistema, como o banco 
de dados e a aplicação.

Prometheus:
- Integrar Prometheus ao aplicativo para coletar métricas como latência das 
requisições, uso de CPU e memória.
- Configurar os endpoints de métricas no aplicativo.

Grafana:
- Configurar um dashboard em Grafana para visualizar as métricas 
coletadas pelo Prometheus.
- Criar painéis para exibir métricas específicas como latência por endpoint, 
contagem de requisições por status de resposta, uso de recursos do 
sistema etc.
