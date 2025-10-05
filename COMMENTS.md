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

## 6. Melhorias Futuras

### 🔒 Backend

- Adicionar **autenticação JWT** para segurança.
- Implementar **testes de interface** com **Selenium**.
- Criar **testes de integração** entre módulos.

---
