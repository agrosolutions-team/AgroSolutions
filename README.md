# 📋 Justificativa Técnica e Requisitos Não-Funcionais - AgroSolutions IoT

## 1. JUSTIFICATIVA TÉCNICA DAS DECISÕES ARQUITETURAIS

### 1.1 Arquitetura em Camadas (Clean Architecture + DDD)

**Decisão:** Adoção de Clean Architecture com 4 camadas (Domain, Application, Infrastructure, Api)

**Justificativa:**
- **Separação de Responsabilidades**: Cada camada possui um propósito bem definido, facilitando manutenção e evolução
- **Testabilidade**: A estrutura em camadas permite testes unitários isolados em cada nível
- **Escalabilidade**: Novas funcionalidades podem ser adicionadas sem impactar o código existente
- **Domain-Driven Design (DDD)**: Alinhamento com a linguagem ubíqua do domínio agrícola

**Estrutura Implementada:**
```
Domain → Entidades e contratos (interfaces)
Application → Casos de uso, DTOs e serviços
Infrastructure → Persistência (EF Core, PostgreSQL)
Api → Controllers e configurações
```

### 1.2 Microserviços Independentes

**Decisão:** Estrutura com múltiplos microserviços (Identidade, Propriedades, Ingestão de Dados, Alertas)

**Justificativa:**
- **Desacoplamento**: Cada serviço é independente e pode ser deployado separadamente
- **Escalabilidade Horizontal**: Serviços críticos podem ser escalados individualmente
- **Resiliência**: Falha em um serviço não afeta os demais
- **Desenvolvimento Paralelo**: Equipes diferentes podem trabalhar em paralelo

**Serviços Identificados:**
1. **AgroSolutions.IoT.Identidade**: Autenticação e gestão de usuários
2. **AgroSolutions.IoT.Propriedades**: Gerenciamento de propriedades rurais e talhões
3. **AgroSolutions.IoT.IngestaoDados**: Processamento de dados de sensores
4. **AgroSolutions.IoT.Alertas**: Geração e notificação de alertas

### 1.3 Comunicação Assíncrona com RabbitMQ

**Decisão:** Implementação de Message Broker para desacoplamento entre serviços

**Justificativa:**
- **Desacoplamento Temporal**: Os serviços não precisam estar sincronizados
- **Confiabilidade**: Mensagens são persistidas e processadas mesmo se o consumidor estiver indisponível
- **Escalabilidade**: Múltiplos consumidores podem processar a mesma mensagem
- **Padrão Publish-Subscribe**: Permite um-para-muitos comunicação

### 1.4 Banco de Dados Centralizado (PostgreSQL)

**Decisão:** Utilização de PostgreSQL como SGBD central

**Justificativa:**
- **ACID Compliance**: Garante consistência de dados críticos
- **Performance**: Excelente performance para queries complexas
- **Suporte a UUID**: Geração nativa de UUIDs sem dependência de aplicação
- **Extensibilidade**: Suporte a tipos de dados customizados para domínio agrícola
- **Migrations com EF Core**: Versionamento automático do esquema

### 1.5 Autenticação e Autorização com JWT

**Decisão:** JWT (JSON Web Tokens) para autenticação sem estado

**Justificativa:**
- **Stateless**: Reduz carga no servidor
- **Escalável**: Compatível com múltiplas instâncias
- **Seguro**: Tokens assinados digitalmente com HS256
- **Standard**: Amplamente adotado na indústria

### 1.6 Segurança de Senhas com BCrypt

**Decisão:** BCrypt.Net para hashing de senhas

**Justificativa:**
- **Salt Automático**: Cada hash inclui um salt único
- **Iterações Adaptativas**: Resiste a ataques de força bruta
- **Não Reversível**: Impossível recuperar a senha original
- **Padrão de Mercado**: Recomendado por especialistas em segurança

### 1.7 Injeção de Dependências (DI)

**Decisão:** Container nativo do ASP.NET Core

**Justificativa:**
- **Testabilidade**: Facilita mocking de dependências
- **SOLID Principles**: Respeita princípio de Inversão de Controle
- **Performance**: Sem overhead de containers externos
- **Type Safety**: Validação de tipos em tempo de compilação

---

## 2. REQUISITOS NÃO-FUNCIONAIS E COMO SERÃO ATENDIDOS

### 2.1 PERFORMANCE

#### Objetivo: Tempo de resposta < 500ms para 95% das requisições

**Implementações:**

1. **Entity Framework Core com Query Optimization**
   - Uso de `.AsNoTracking()` para queries read-only
   - Índices no PostgreSQL para campos frequentemente consultados
   - Lazy loading controlado para evitar N+1 queries

2. **Compressão HTTP**
   - Gzip habilitado para respostas de API
   - Redução de bandwidth em transmissões de dados

3. **New Relic Integration**
   - Monitoramento contínuo de performance
   - Alertas automáticos para degradação

### 2.2 ESCALABILIDADE

#### Objetivo: Suportar crescimento de usuários e dados sem redesign

**Implementações:**

1. **Containerização com Docker**
   - Multi-stage builds para otimizar tamanho de imagem
   - Ambiente configurável via variáveis

2. **Orquestração com Docker Compose**
   - Escalabilidade horizontal possível com replicas
   - Service discovery automático via rede
   - Health checks em todos os serviços

3. **Arquitetura de Microserviços**
   - Permite escalar serviços independentemente
   - Exemplo: Se Ingestão de Dados fica sobrecarregada, apenas esse serviço é replicado

4. **Padrão Assíncrono com RabbitMQ**
   - Desacopla produtor de consumidor
   - Múltiplos consumidores podem processar em paralelo

5. **Índices de Banco de Dados**
   - PostgreSQL gera automaticamente:
   - Primary Key index em Id (UUID)
   - Foreign Key constraints
   - Índices em campos frequentemente filtrados

### 2.3 CONFIABILIDADE

#### Objetivo: 99.5% de uptime, recuperação automática de falhas

**Implementações:**

1. **Health Checks em Docker Compose**

2. **Tratamento de Exceções Estruturado**
   - ErrorHandlingMiddleware em todos os serviços
   - Respostas HTTP padronizadas (400, 404, 500, etc.)

3. **Validação de Dados em Múltiplas Camadas**
   - Domain: Entidades ricas com auto-validação
   - Application: DTOs com DataAnnotations
   - API: Swagger/OpenAPI para documentação de contratos

4. **Persistência de Dados Críticos**
   - PostgreSQL com volumes persistentes
   - Dados de sensores salvos antes de processar
   - Transações ACID garantem consistência

### 2.4 SEGURANÇA

#### Objetivo: Proteger dados de usuários e não autorizar acesso indevido

**Implementações:**

1. **Autenticação com JWT**
   - Token inclui claims: sub (userId), email, nome
   - Validação em todos os endpoints protegidos
   - Expiração configurável (padrão: 2 horas)

2. **Autorização Baseada em Claims**

3. **Validação de Propriedade de Recursos**
   - Regra de Negócio: "Apenas o produtor dono pode acessar suas propriedades"
   - Implementado na Application Layer via ProdutorId extraído do JWT
  
4. **Criptografia de Senhas com BCrypt**
   - Hashing irreversível
   - Salt único por usuário
   - Comparação segura contra timing attacks

5. **HTTPS/TLS**

6. **Proteção contra Ataques Comuns**
   - SQL Injection: EF Core parametriza queries
   - XSS: DTOs não incluem HTML scripts
   - CSRF: Token validation em modificações (POST, PUT, DELETE)

7. **Variáveis Sensíveis em Environment**
   - JWT SecretKey: não hardcoded
   - Connection String: vem de configuration
   - New Relic License: via variável de ambiente

### 2.5 DISPONIBILIDADE

#### Objetivo: Serviço acessível 24/7, mesmo em caso de falha parcial

**Implementações:**

1. **Redundância de Serviços**
   - Cada serviço pode ter múltiplas instâncias
   - Load balancer distribui requisições

2. **Graceful Degradation**
   - Se AlertasService falhar, ingestão continua funcionando
   - Mensagens ficam em fila até consumidor recuperar

3. **Circuit Breaker Pattern**
   - Evita requests a serviço indisponível
   - Falha rápida em vez de timeout

4. **Database Connection Pooling**
   - Reutilização de conexões
   - Melhor utilização de recursos

### 2.6 MANUTENIBILIDADE

#### Objetivo: Código limpo, bem documentado e fácil de estender

**Implementações:**

1. **Clean Code e SOLID Principles**
   - Single Responsibility: Cada classe tem um motivo para mudar
   - Open/Closed: Aberto para extensão, fechado para modificação
   - Liskov Substitution: Interfaces bem definidas
   - Interface Segregation: Interfaces pequenas e específicas
   - Dependency Inversion: Depender de abstrações, não implementações

2. **Documentação com Swagger/OpenAPI**

3. **Testes Unitários**

4. **Versionamento de Banco de Dados**
   - EF Core Migrations para rastreabilidade
   - Up/Down migrations para rollback seguro

5. **Code Organization**
   - Estrutura em camadas facilita localização de código
   - Naming conventions consistentes
   - DTOs para contrato entre camadas

### 2.7 AUDITORIA E COMPLIANCE

#### Objetivo: Rastrear quem fez o quê e quando

**Implementações:**

1. **Logging Centralizado com Serilog**

2. **Rastreabilidade de Dados**

3. **New Relic Monitoring**
   - Rastreamento de requests
   - Identificação de transações lentas
   - Análise de erros
---

