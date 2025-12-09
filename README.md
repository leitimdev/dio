# Portfólio de Projetos - DIO (Digital Innovation One)

Este repositório contém diversos projetos desenvolvidos durante os bootcamps e cursos da Digital Innovation One, abrangendo múltiplas tecnologias e stacks de desenvolvimento.

## 📑 Índice de Projetos

### 🔷 Projetos .NET / C#

#### 1. **DesafioTecnico** - Arquitetura de Microserviços
Sistema completo de gestão de estoque e vendas com arquitetura de microserviços.

**Stack Utilizada:**
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- RabbitMQ (Mensageria)
- YARP (API Gateway)
- JWT (Autenticação)
- Docker & Docker Compose
- Swagger/OpenAPI

**Microserviços:**
- AuthService (porta 7001) - Autenticação e autorização
- EstoqueService (porta 7002) - Gestão de produtos
- VendasService (porta 7003) - Processamento de vendas
- API Gateway (porta 7000) - Roteamento centralizado

---

#### 2. **MinimalAPI** - API com Minimal APIs
Projeto utilizando ASP.NET Core Minimal API para gestão de veículos e administradores.

**Stack Utilizada:**
- ASP.NET Core Minimal API
- Entity Framework Core
- MySQL
- JWT Bearer Authentication
- Swagger/OpenAPI
- Clean Architecture

**Funcionalidades:**
- CRUD de Administradores
- CRUD de Veículos
- Autorização baseada em roles

---

#### 3. **GerenciadorTarefas** - Sistema de Organização de Tarefas
API para gerenciamento de tarefas com persistência em banco de dados.

**Stack Utilizada:**
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger/OpenAPI
- JSON serialization com enums

---

#### 4. **ModuloAPI** - Agenda de Contatos
API para gerenciamento de agenda de contatos.

**Stack Utilizada:**
- ASP.NET Core Web API
- Entity Framework Core
- MySQL
- Swagger/OpenAPI

---

#### 5. **WebAPI** - API de Demonstração
API de exemplo com endpoints de previsão do tempo e outros recursos.

**Stack Utilizada:**
- ASP.NET Core (.NET 9)
- OpenAPI
- Swagger/SwaggerUI
- Controllers

---

#### 6. **Desafio-Id-BandeiraCartao** - Identificador de Bandeira de Cartão
Aplicação console que identifica a bandeira do cartão de crédito baseado no número.

**Stack Utilizada:**
- C# Console Application
- .NET Core
- Algoritmos de validação (Visa, Mastercard, Amex, etc.)

**Funcionalidades:**
- Validação de números de cartão
- Identificação de bandeiras (Visa, Mastercard, American Express, Diners Club, Discover, JCB, Elo, Hipercard)
- Remoção automática de espaços e hífens

---

#### 7. **SistemaEstacionamento** - Sistema de Gerenciamento de Estacionamento
Sistema console para gerenciar veículos em estacionamento.

**Stack Utilizada:**
- C# Console Application
- .NET 9
- Collections (List)

**Funcionalidades:**
- Adicionar veículos
- Remover veículos
- Listar veículos
- Calcular valores com base em tempo de permanência

---

#### 8. **Celular** - Sistema de Smartphones (POO)
Projeto demonstrando conceitos de Programação Orientada a Objetos.

**Stack Utilizada:**
- C# Console Application
- .NET Core
- POO (Herança, Polimorfismo, Abstração)

**Funcionalidades:**
- Classes abstratas (Smartphone)
- Implementações concretas (Nokia, iPhone)
- Métodos polimórficos (InstalarAplicativo)

---

#### 9. **ExemploFundamentos** - Fundamentos de C#
Projeto educacional cobrindo fundamentos da linguagem C#.

**Stack Utilizada:**
- C# (.NET 5, .NET 6)
- .NET Core
- Collections (List, Arrays)

**Tópicos Abordados:**
- Listas e Arrays
- Loops (for, foreach)
- Manipulação de collections
- Tipos de dados

---

#### 10. **EstudoPOO** e **EstudoPOO_v2** - Estudos de POO
Projetos de estudo de Programação Orientada a Objetos.

**Stack Utilizada:**
- C# Console Application
- .NET Core
- Conceitos de POO

---

#### 11. **Hotel** - Sistema de Gerenciamento de Hotel
Sistema para gerenciamento de reservas e hóspedes.

**Stack Utilizada:**
- C# Console Application
- .NET Core
- POO

---

#### 12. **Testes** - Projeto de Testes
Projeto para estudos de testes unitários e boas práticas.

**Stack Utilizada:**
- C# .NET 9
- xUnit ou NUnit (framework de testes)

---

### ☁️ Projetos Cloud

#### 13. **AzureDesafio** - Análise de Sentimento com Azure AI
Projeto demonstrando o uso de serviços de IA do Azure.

**Stack Utilizada:**
- Azure AI Foundry
- Azure Cognitive Services
- Azure Machine Learning
- Análise de Sentimento (NLP)
- Azure Portal

**Recursos Criados:**
- Workspace Azure ML
- Cognitive Services (Speech/Text)
- Análise de sentimento em português

---

#### 14. **Desafio-Azure-Docker** - Docker com Azure
Aplicação PHP containerizada com Docker e MySQL.

**Stack Utilizada:**
- Docker
- Nginx
- PHP
- MySQL
- Azure (deploy)

**Arquivos:**
- Dockerfile customizado
- nginx.conf
- Aplicação PHP com conexão ao banco

---

#### 15. **aws-cloud** - Scripts de Infraestrutura AWS/Linux
Scripts para automação de infraestrutura e gerenciamento de usuários.

**Stack Utilizada:**
- Bash Script
- Linux (comandos de sistema)
- AWS (conceitos)
- OpenSSL

**Funcionalidades:**
- Criação automática de diretórios
- Gerenciamento de grupos de usuários
- Criação de usuários com senhas criptografadas
- Configuração de permissões

---

### ⚛️ Projetos Front-end

#### 16. **hello-world-react** - Aplicação React
Projeto Hello World demonstrando conceitos básicos do React.

**Stack Utilizada:**
- React 19
- JavaScript ES6+
- CSS3
- Create React App
- React Hooks
- Components
- Props
- Event Handlers

**Funcionalidades:**
- Componente HelloWorld personalizado
- Estado dinâmico
- Styling inline
- Data atual dinâmica

---

### ☕ Projetos Java

#### 17. **java-amdocs** - Bootcamp Java Amdocs
Coleção de projetos do bootcamp Java.

**Stack Utilizada:**
- Java
- Spring Boot
- Spring Cloud (Feign)
- Cucumber (BDD)
- Maven/Gradle
- Injeção de Dependência
- Inversão de Controle
- POO

**Subprojetos:**
- cucumber-bruno - Testes BDD
- desafio-banco - Sistema bancário
- desafio-final-personapi - API de pessoas
- springboot - Aplicações Spring
- projetoonefeign1/2 - Integração com Feign Client
- utilizandoBeans - Configuração de Beans

---

#### 18. **java-santander** - Bootcamp Java Santander
Projetos do bootcamp Santander.

**Stack Utilizada:**
- Java
- JUnit (Testes unitários)
- Mockito (Mocks)
- Stream API
- Collections (List, Set, Map)
- Comparable e Comparator
- POO avançado

**Subprojetos:**
- junit-aulateste - Exemplos de testes
- mockito-exemplos - Testes com mocks
- StreamAPI - Programação funcional
- ListSetMap - Estruturas de dados
- comparableXcomparator - Comparação de objetos
- Desafio-Dio-Poo - Desafio de POO

---

#### 19. **java-kotlin-sportheca** - Estruturas de Dados
Implementações de estruturas de dados em Java.

**Stack Utilizada:**
- Java
- Estruturas de Dados
- Algoritmos

**Implementações:**
- Árvore Binária
- Listas
- Pilhas (Stack)
- Filas (Queue)
- Mapas (Map)
- Conjuntos (Set)
- Equals e HashCode

---

### 🤖 Projetos de Machine Learning

#### 20. **dp100** - Certificação Azure Data Scientist
Notebook Jupyter para treinamento de modelos de Machine Learning.

**Stack Utilizada:**
- Python
- Jupyter Notebook
- pandas
- numpy
- scikit-learn
- Logistic Regression
- Machine Learning

**Projeto:**
- Sorvetes.ipynb - Previsão de vendas baseada em temperatura
- Split de dados (treino/teste)
- Métricas de avaliação (ROC-AUC, Accuracy)

---

### 🎨 Projetos de IA Generativa

#### 21. **lab-natty-or-not-main** - IA Generativa
Projeto explorando ferramentas de IA generativa.

**Stack Utilizada:**
- Perplexity AI
- Claude AI (GitHub Copilot)
- Python (JuntaPDF)

**Funcionalidades:**
- Transformação de imagens em desenho Giphy
- Projeto JuntaPDF - Unificação de PDFs com IA
- Uso de IA para aumentar produtividade

---

## 📊 Resumo de Tecnologias

### Linguagens
- C# / .NET (Core, 5, 6, 8, 9)
- Java
- Python
- JavaScript/TypeScript
- PHP
- Bash

### Frameworks e Libraries
- ASP.NET Core (Web API, Minimal API)
- React
- Spring Boot
- Entity Framework Core
- Scikit-learn
- Pandas/Numpy

### Bancos de Dados
- MySQL
- SQL Server (implícito)

### Cloud e DevOps
- Azure (AI, ML, Cognitive Services)
- AWS (conceitos)
- Docker
- Docker Compose
- Nginx

### Mensageria e APIs
- RabbitMQ
- REST APIs
- YARP (API Gateway)
- Feign Client

### Ferramentas de Desenvolvimento
- Swagger/OpenAPI
- JUnit
- Mockito
- Cucumber (BDD)
- Git/GitHub

### Conceitos Aplicados
- Microserviços
- Clean Architecture
- POO (Programação Orientada a Objetos)
- JWT Authentication
- Machine Learning
- Estruturas de Dados
- Testes Unitários
- IA Generativa

---

## 🎓 Sobre

Estes projetos foram desenvolvidos como parte dos bootcamps e cursos da **Digital Innovation One (DIO)**, abrangendo desde conceitos básicos até arquiteturas avançadas de software, cloud computing e inteligência artificial.

---

**Última atualização:** Dezembro 2025
