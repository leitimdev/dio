# Desafio Técnico - Arquitetura de Microserviços

## Visão Geral

Este projeto implementa uma arquitetura de microserviços completa para um sistema de gestão de estoque e vendas, desenvolvido como parte de um desafio técnico. O sistema foi migrado de uma arquitetura monolítica para microserviços, implementando as melhores práticas de desenvolvimento distribuído.

## Arquitetura

### Microserviços

1. **AuthService** (porta 7001)
   - Autenticação e autorização com JWT
   - Gestão de usuários
   - Endpoints: `/api/auth/login`, `/api/auth/register`, `/api/auth/validate`

2. **EstoqueService** (porta 7002)
   - Gestão de produtos e estoque
   - Validação de disponibilidade
   - Endpoints: `/api/estoque/*`

3. **VendasService** (porta 7003)
   - Gestão de vendas
   - Processamento de pedidos
   - Endpoints: `/api/vendas/*`

4. **API Gateway** (porta 7000)
   - Roteamento centralizado usando YARP
   - Autenticação centralizada
   - Load balancing

### Infraestrutura

- **MySQL**: Banco de dados (bancos separados por microserviço)
- **RabbitMQ**: Mensageria assíncrona entre microserviços
- **Docker Compose**: Orquestração de containers

## Tecnologias Utilizadas

- **.NET 8.0**: Framework principal
- **ASP.NET Core**: Web API
- **Entity Framework Core**: ORM
- **MySQL**: Banco de dados
- **RabbitMQ**: Message broker
- **YARP**: Reverse proxy para API Gateway
- **JWT**: Autenticação
- **Docker**: Containerização
- **Swagger**: Documentação da API

## Comunicação Entre Microserviços

### Eventos RabbitMQ

1. **VendaCriadaEvent**: Quando uma venda é criada
2. **EstoqueValidadoEvent**: Resultado da validação de estoque
3. **VendaConfirmadaEvent**: Quando uma venda é confirmada

### Fluxo de Comunicação

1. Cliente cria uma venda → VendasService
2. VendasService publica `VendaCriadaEvent`
3. EstoqueService consome o evento e valida o estoque
4. EstoqueService publica `EstoqueValidadoEvent`
5. VendasService consome e confirma/cancela a venda

## Configuração e Execução

### Pré-requisitos

- Docker e Docker Compose
- .NET 8.0 SDK (para desenvolvimento local)

### Executar com Docker Compose

```bash
# Clonar o repositório
git clone <url-do-repositorio>
cd DesafioTecnico

# Executar todos os serviços
docker-compose up -d

# Verificar status dos containers
docker-compose ps

# Ver logs
docker-compose logs -f
```

### Executar Localmente (Desenvolvimento)

```bash
# Terminal 1 - AuthService
cd Microservices/AuthService/DesafioTecnico.AuthService
dotnet run

# Terminal 2 - EstoqueService
cd Microservices/EstoqueService/DesafioTecnico.EstoqueService
dotnet run

# Terminal 3 - VendasService
cd Microservices/VendasService/DesafioTecnico.VendasService
dotnet run

# Terminal 4 - API Gateway
cd ApiGateway/DesafioTecnico.ApiGateway
dotnet run
```

## Endpoints Principais

### Via API Gateway (porta 7000)

- **Health Check**: `GET /health`
- **Gateway Info**: `GET /gateway/info`
- **Autenticação**: `POST /api/auth/login`
- **Produtos**: `GET /api/estoque`
- **Vendas**: `GET /api/vendas`

### Documentação Swagger

- API Gateway: `https://localhost:7000`
- AuthService: `https://localhost:7001/swagger`
- EstoqueService: `https://localhost:7002/swagger`
- VendasService: `https://localhost:7003/swagger`

## Banco de Dados

Cada microserviço possui seu próprio banco de dados:

- `desafiotecnico_auth`: Usuários e autenticação
- `desafiotecnico_estoque`: Produtos e estoque
- `desafiotecnico_vendas`: Vendas e pedidos

## Segurança

- **JWT Tokens**: Autenticação stateless
- **CORS**: Configurado para desenvolvimento
- **HTTPS**: Certificados para ambiente de desenvolvimento

## Monitoramento

- **RabbitMQ Management**: `http://localhost:15672` (guest/guest)
- **Container Logs**: `docker-compose logs <service-name>`
- **Health Checks**: Endpoint `/health` em cada serviço

## Estrutura do Projeto

```
DesafioTecnico/
├── Microservices/
│   ├── AuthService/
│   ├── EstoqueService/
│   └── VendasService/
├── ApiGateway/
├── Shared/
│   └── DesafioTecnico.Shared/
├── docker-compose.yml
└── README.md
```

## Desenvolvimento

### Adicionando Novos Microserviços

1. Criar novo projeto na pasta `Microservices`
2. Referenciar `DesafioTecnico.Shared`
3. Implementar interfaces de messaging
4. Adicionar configuração no API Gateway
5. Criar Dockerfile
6. Atualizar docker-compose.yml

### Adicionando Novos Eventos

1. Definir evento em `Shared/Events`
2. Implementar publisher no microserviço origem
3. Implementar consumer no microserviço destino

## Compliance com Requisitos Técnicos

✅ **Arquitetura de Microserviços**: Implementada com separação clara de responsabilidades
✅ **API Gateway**: YARP para roteamento centralizado
✅ **Comunicação Assíncrona**: RabbitMQ para eventos
✅ **Bancos de Dados Separados**: MySQL por microserviço
✅ **Containerização**: Docker e Docker Compose
✅ **Autenticação**: JWT centralizada
✅ **Documentação**: Swagger em todos os serviços
✅ **Padrões REST**: Implementados em todas as APIs
✅ **CORS**: Configurado apropriadamente

## Próximos Passos

- [ ] Implementar circuit breaker (Polly)
- [ ] Adicionar métricas e logging centralizado
- [ ] Implementar testes automatizados
- [ ] Configurar CI/CD pipeline
- [ ] Adicionar monitoring com Prometheus/Grafana