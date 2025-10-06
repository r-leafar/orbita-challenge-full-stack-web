# 🐳 Guia de Execução — Docker Compose (Backend + Frontend + Banco de Dados)

Este `docker-compose.yaml` orquestra **três serviços principais**:  
1. **Backend (ASP.NET Web API)**  
2. **Frontend (Vue + Vite)**  
3. **Banco de Dados (PostgreSQL)**  

O objetivo é permitir o funcionamento completo da aplicação **com um único comando**, conectando todas as partes automaticamente.

---

## ⚙️ 1. Estrutura Geral dos Serviços

### 🔹 `service_webapi`
- Constrói o **backend** a partir da pasta `./backend/EdTech`.
- Expõe a porta configurada em `${WEBAPI_HTTP_PORT}`.
- Conecta-se ao banco **PostgreSQL** através do nome do serviço `service_postgres`.
- Usa variáveis de ambiente para configurar conexão e URLs.

### 🔹 `service_frontend`
- Constrói o **frontend Vue/Vuetify** localizado em `./frontend`.
- Roda o **Nginx** dentro do container e expõe a porta `${VITE_PORT}` local.
- Se comunica com o backend via `VITE_APP_API_URL: http://service_webapi:${WEBAPI_HTTP_PORT}`.

### 🔹 `service_postgres`
- Usa a imagem oficial `postgres:17.6`.
- Persiste os dados em um volume local (`postgres_data`).
- Executa scripts SQL de inicialização da pasta `./initdb`.

---

## 📦 2. Arquivo `.env` Necessário

O Docker Compose utiliza variáveis de ambiente definidas em um arquivo chamado **`.env`**, localizado na raiz do projeto.  
Essas variáveis são usadas para configurar portas, credenciais e URLs de comunicação entre os serviços.

###  Como configurar

1. Localize o arquivo **`sample.env`** existente na raiz do projeto.  
2. Renomeie-o para **`.env`** (sem o `sample`):  
   ```bash
   mv sample.env .env
   ````
## 🚀 3. Executando o Projeto
▶️ Passo 1 — Construir e iniciar os containers
Execute o comando abaixo na raiz do projeto (onde está o `docker-compose.yaml`):
```bash
docker compose up --build
```
1. --build garante que as imagens do frontend e backend sejam recompiladas.
2. O processo pode demorar na primeira execução.

## ✅ Verificação
Após o build:

🌐 Frontend acessível em: http://localhost:${VITE_PORT}

⚙️ API acessível em: http://localhost:${HOST_HTTP_PORT}

🗄️ Banco PostgreSQL rodando localmente na porta ${POSTGRES_PORT}

Se tudo estiver correto, o frontend deve comunicar-se automaticamente com o backend via a URL definida em VITE_APP_API_URL
---
# Documentação do Backend (ASP.NET Core - Minimal API)

Esta seção descreve a arquitetura, as funcionalidades e a organização do backend da aplicação de **gerenciamento de alunos**, desenvolvida em **C# com ASP.NET Core**.

---

## 1. Estrutura do Projeto

Foi adotada a **Clean Architecture**, uma solução que divide a aplicação em camadas com responsabilidades específicas, promovendo **baixo acoplamento** e **alta coesão**.  
Além disso, são aplicadas boas práticas como **CQRS** e os princípios **SOLID**.

### 🧩 CQRS

O padrão **CQRS (Command Query Responsibility Segregation)** separa as operações de **leitura de dados (Queries)** das operações de **modificação de dados (Commands)**, melhorando a organização e a escalabilidade.

### 🧠 Princípios SOLID

Os princípios **SOLID** facilitam a manutenção, o entendimento e a expansão do código ao longo do tempo:

1. **S**ingle Responsibility Principle — Cada classe deve ter uma única responsabilidade.
2. **O**pen/Closed Principle — O código deve estar aberto para extensão, mas fechado para modificação.
3. **L**iskov Substitution Principle — Subtipos devem poder substituir seus tipos base sem alterar o comportamento.
4. **I**nterface Segregation Principle — Interfaces específicas são preferíveis a interfaces genéricas e grandes.
5. **D**ependency Inversion Principle — Dependa de abstrações, não de implementações concretas.

---

## 2. Camadas do Projeto

### **EdTech.Core**

- **Responsabilidade:** Regras de negócio e domínio.
- **Características:** Contém entidades e regras de negócio; não depende de outras camadas.
- **Comunicação:** Expõe interfaces para interação com o mundo externo.

### **EdTech.Application**

- **Responsabilidade:** Casos de uso da aplicação.
- **Características:** Coordena a lógica de negócios, orquestrando comandos e queries sem depender da infraestrutura.

### **EdTech.Infrastructure**

- **Responsabilidade:** Implementações de suporte.
- **Características:** Contém acesso a banco de dados, serviços externos e persistência; não contém regras de negócio.

### **EdTech.WebApi**

- **Responsabilidade:** Interação com o cliente (API HTTP).
- **Características:** Recebe requisições, envia respostas e delega a lógica para as camadas inferiores.

### **EdTech.UnitTest**

- **Responsabilidade:** Validação e testes.
- **Características:** Contém testes automatizados que verificam regras de negócio e previnem regressões.

---

## 3. Modelagem de Dados

A entidade **`Student`** possui as seguintes propriedades:

| Propriedade          | Tipo                 | Descrição                           |
| -------------------- | -------------------- | ----------------------------------- |
| `Id`                 | `Guid`               | Identificador único                 |
| `Name`               | `string`             | Nome do aluno                       |
| `Email`              | `string`             | E-mail do aluno                     |
| `StudentId`          | `string`             | Identificação escolar               |
| `NationalIdentifier` | `NationalIdentifier` | Documento de identificação nacional |

O campo **CPF** é armazenado em `NationalIdentifier`, uma **classe abstrata**.  
A implementação concreta `CpfIdentifier` define regras específicas para CPF, simulando a possibilidade de suportar **diferentes identificadores por país**.  
Esse design segue o **padrão Strategy**, facilitando extensão e manutenção sem aumentar a complexidade.

### Estrutura das Tabelas

#### **students**

| Coluna     | Tipo   | Constraint |
| ---------- | ------ | ---------- |
| id         | Guid   | PK         |
| name       | string |            |
| email      | string |            |
| student_id | string | UK         |

#### **national_identifier**

| Coluna          | Tipo   | Constraint |
| --------------- | ------ | ---------- |
| student_id      | Guid   | PK / FK    |
| number          | string |            |
| identifier_type | string |            |

---

## 4. Endpoints da API

| Método HTTP | Endpoint                             | Descrição                           | Parâmetros                                                     | Retorno                          |
| ----------- | ------------------------------------ | ----------------------------------- | -------------------------------------------------------------- | -------------------------------- |
| `GET`       | `api/v1/students/{pagina}/{tamanho}` | Retorna a lista de alunos paginada. | `pagina`: número da página<br>`tamanho`: quantidade por página | `PagedResponse<StudentResponse>` |
| `GET`       | `api/v1/students/{id}`               | Retorna um aluno específico.        | `id` (`Guid`)                                                  | `StudentResponse`                |
| `POST`      | `api/v1/students`                    | Cadastra um novo aluno.             | `CreateStudentRequest` no corpo da requisição                  | `201 Created`                    |
| `PUT`       | `api/v1/students`                    | Atualiza um aluno existente.        | `UpdateStudentRequest` no corpo da requisição                  | `204 No Content`                 |
| `DELETE`    | `api/v1/students/{id}`               | Exclui um aluno.                    | `id` (`Guid`)                                                  | `204 No Content`                 |

---

### 📨 Exemplo de Body - `POST /api/v1/students`

```json
{
  "Name": "João da Silva",
  "Email": "joao.silva@example.com",
  "StudentId": "123456",
  "NationalIdType": "CPF",
  "NationalIdValue": "12345678900"
}
```

---

### 📨 Exemplo de Body - `PUT /api/v1/students`

```json
{
  "Id": "0199b29f-ba99-7fec-81de-a54ffd6c7610",
  "Name": "joão Pereira da Silva",
  "Email": "joao.pereira@example.com"
}
```

---

## 5. Pacotes Utilizados

| Pacote                                  | Descrição                                          |
| --------------------------------------- | -------------------------------------------------- |
| `EFCore.NamingConventions`              | Garante nomes de tabelas e colunas em minúsculas.  |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | Provedor do PostgreSQL para Entity Framework Core. |
| `Moq`                                   | Mocking framework para testes unitários.           |
| `xUnit`                                 | Framework de testes unitários.                     |
| `Microsoft.EntityFrameworkCore.Tools`   | Ferramentas CLI para criação de migrations.        |

---

## 6. Melhorias Futuras 🚀

### 🔒 Backend

- Adicionar **autenticação JWT** para segurança.
- Implementar **testes de interface** com **Selenium**.
- Criar **testes de integração** entre módulos.

---
# Documentação do Frontend (Vue + Vuetify)

O projeto segue a estrutura padrão do **Vue 3**, utilizando **Componentes de Arquivo Único (.vue)** para representar cada tela.  
Essa abordagem, combinada com um **roteador dedicado**, melhora a organização, manutenção e extensibilidade do sistema, permitindo mapear claramente as rotas e os componentes responsáveis por cada visualização.

---

## 1. 📁 Estrutura de Pastas

### 🧠 Lógicas de Negócio (Stores)
- **`src/stores/studentStore.js`** — Responsável por toda a lógica de negócio relacionada aos alunos.  
- **`src/stores/notificationStore.js`** — Gerencia mensagens e notificações exibidas na interface.  
- **`src/stores/authUserStore.js`** — Controla a autenticação do usuário (simulação de login/logout).

---

### ⚙️ Funções Reutilizáveis (Composables)
- **`src/composables/useAuth.js`** — Contém funções para autenticação (login e logout).  
- **`src/composables/useForm.js`** — Fornece funções utilitárias para validação de formulários.

---

### 🧩 Componentes Utilizados
- **`src/components/AppMenuBar.vue`** — Componente do menu principal de navegação.  
- **`src/components/ConfirmDialog.vue`** — Diálogo genérico de confirmação de ações.  
- **`src/components/StudentForm.vue`** — Formulário responsável por registrar e atualizar dados de alunos.

---

### 🖥️ Páginas
- **`"/"`** — Página de login. Caso o usuário já esteja autenticado, redireciona para **Gerenciar Alunos**.  
- **`"/management-students"`** — Página principal de gerenciamento de alunos.

---

### 🧾 Tipos
- **`src/types/*`** — Contém os tipos e interfaces TypeScript utilizados em toda a aplicação.
---
## 2. Pacotes Utilizados
| Pacote                         | Descrição |
| ------------------------------- | ---------- |
| **`axios`**                    | Utilizado para realizar requisições HTTP de forma simples e eficiente. |
| **`pinia`**                    | Biblioteca oficial de gerenciamento de estado do Vue 3, sucessora do Vuex. |
| **`pinia-plugin-persistedstate`** | Plugin do Pinia que permite persistir estados no `localStorage`, mantendo dados entre recarregamentos da página. |
| **`vuetify`**                  | Framework de componentes UI baseado em Material Design, utilizado para criação de interfaces modernas e responsivas. |
| **`vue-router`**               | Biblioteca oficial de roteamento do Vue, responsável pelo controle de navegação entre as páginas da aplicação. |

## 3. Melhorias Futuras 🚀

### 🔒 Frontend
- Migrar o armazenamento local para **cookies HTTP-only**, aumentando a segurança contra XSS.  
- Melhorar o sistema de **paginação**, exibindo controles apenas quando houver registros disponíveis.  
- **Refatorar o código**, priorizando legibilidade e reutilização de componentes.  
- Implementar **testes unitários e de interface**, garantindo maior confiabilidade e cobertura do sistema.