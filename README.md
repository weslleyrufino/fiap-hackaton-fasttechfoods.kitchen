# FIAP PÓS TECH - Fiap-Hackaton-FastTechFoods - Kitchen

### 🧠 Sobre o Projeto
Este microsserviço foi desenvolvido de forma individual, utilizando os princípios da Clean Architecture com forte inspiração em Domain-Driven Design (DDD).
A estrutura foi pensada para garantir baixo acoplamento, alta coesão e facilidade de manutenção.

### 🔧 Tecnologias e padrões utilizados:
.NET 8 com C#

Entity Framework Core para persistência de dados

RabbitMQ para comunicação assíncrona entre microsserviços

Clean Architecture com separação de responsabilidades em:

API: camada de entrada (Controllers, Program, Configs)

Application: serviços de aplicação, view models, validações e interfaces

Domain: entidades e lógica de negócio pura

Infrastructure: repositórios, configuração de banco e mensagens

Validação com Data Annotations

Mensageria desacoplada via MassTransit + RabbitMQ

### 🎯 Objetivo
Construir um microsserviço autônomo, que possa se comunicar com outros serviços via mensagens (RabbitMQ) e seguir boas práticas de arquitetura, visando escalabilidade, testabilidade e clareza de responsabilidades.

### 🔧 Como rodar o projeto localmente

1. Abra o Package Manager Console
2. Selecione como "Default Project": `FastTechFoods.Kitchen.Infrastructure`
3. Rode o comando:
	Update-Database -StartupProject FastTechFoods.Kitchen.API
	
4. Pronto! O banco será criado automaticamente com as tabelas necessárias.

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
