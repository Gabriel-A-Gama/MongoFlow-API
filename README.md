# MongoFlow API

API REST desenvolvida em **ASP.NET Core** com **C#** para gerenciamento de usuários e itens, utilizando **MongoDB** como banco de dados NoSQL e **Docker** para containerização.

---

## Tecnologias

- [ASP.NET Core 9](https://dotnet.microsoft.com/) — Framework web
- [C#](https://learn.microsoft.com/dotnet/csharp/) — Linguagem
- [MongoDB](https://www.mongodb.com/) — Banco de dados NoSQL
- [MongoDB.Driver](https://www.nuget.org/packages/MongoDB.Driver) — Driver oficial do MongoDB para .NET
- [Docker](https://www.docker.com/) — Containerização do banco de dados
- [Scalar](https://scalar.com/) — Documentação interativa da API

---

## Pré-requisitos

Antes de começar, você precisa ter instalado:

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---

## Como rodar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/Gabriel-A-Gama/MongoFlow-API.git
cd mongoflow-api
```

### 2. Suba o MongoDB com Docker

```bash
docker run -d \
  --name mongodb \
  -p 27017:27017 \
  -e MONGO_INITDB_ROOT_USERNAME=admin \
  -e MONGO_INITDB_ROOT_PASSWORD=sua-senha \
  mongo:latest
```

Verifique se o container está rodando:

```bash
docker ps
```

### 3. Configure as variáveis de ambiente

Crie o arquivo `appsettings.json` na raiz do projeto (esse arquivo não é versionado):

```json
{
  "MongoDbConfiguracao": {
    "ConnectionString": "mongodb://admin:sua_senha@localhost:27017",
    "DatabaseName": "NomeDoBanco",
    "UsuariosCollection": "usuarios",
    "ItemsCollection": "itens"
  }
}
```

### 4. Rode o projeto

```bash
dotnet run
```

Acesse a documentação interativa em:

```
https://localhost:7011/scalar/v1
```

---

## Endpoints

### Usuários

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/usuario` | Lista todos os usuários |
| GET | `/api/usuario/{id}` | Busca usuário por ID |
| GET | `/api/usuario/email/{email}` | Busca usuário por e-mail |
| POST | `/api/usuario` | Cria um novo usuário |
| PUT | `/api/usuario/{id}` | Atualiza um usuário |
| DELETE | `/api/usuario/{id}` | Remove um usuário |

### Itens

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/api/item` | Lista todos os itens |
| GET | `/api/item/{id}` | Busca item por ID |
| POST | `/api/item` | Cria um novo item |
| PUT | `/api/item/{id}` | Atualiza um item |
| DELETE | `/api/item/{id}` | Remove um item |

---

##  Estrutura do projeto

```
MongoFlowAPI/
├── Controllers/
│   ├── UsuarioController.cs
│   └── ItemController.cs
├── Models/
│   ├── Usuario.cs
│   └── Item.cs
├── Services/
│   ├── UsuarioService.cs
│   └── ItemService.cs
├── Settings/
│   └── MongoDBConfig.cs
├── appsettings.json - > não versionado  
└── Program.cs
```

---

## Variáveis sensíveis

As credenciais do banco **não são versionadas**. O arquivo `appsettings.json` está no `.gitignore`.
Para rodar o projeto, siga o passo 3 acima e crie o arquivo localmente.

---

## Licença

Este projeto está sob a licença MIT.
