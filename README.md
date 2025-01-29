# API CRUD com MemoryCache

Este projeto é uma API simples em .NET que implementa operações CRUD (Criar, Ler, Atualizar, Deletar) utilizando o `MemoryCache` para cache nas consultas. O objetivo deste projeto é demonstrar como usar o cache em memória para melhorar o desempenho das operações de leitura, evitando chamadas repetidas a uma fonte de dados (simulada neste caso por uma lista de itens na memória).

## Funcionalidades

A API oferece as seguintes funcionalidades:
- **GET /api/items**: Retorna todos os itens (com cache).
- **GET /api/items/{id}**: Retorna um item específico pelo ID (com cache).
- **POST /api/items**: Cria um novo item.
- **PUT /api/items/{id}**: Atualiza um item existente.
- **DELETE /api/items/{id}**: Deleta um item específico.

## Pré-requisitos

Para rodar este projeto, é necessário ter o seguinte instalado em sua máquina:
- [.NET 8.0](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/), caso queira usar um editor de código

## Como rodar o projeto

### 1. Clonar o repositório

Clone o repositório do projeto para sua máquina local:

```bash
https://github.com/gabriela-ncmt/CrudWithMemoryCache.git
```

2. Restaurar pacotes
Restaure os pacotes NuGet para garantir que todas as dependências sejam baixadas:

bash
Copiar
dotnet restore
3. Rodar o projeto
Execute o projeto com o seguinte comando:

```bash

dotnet run
```
Isso irá iniciar o servidor da API. Por padrão, a API estará disponível em http://localhost:7109.

### Endpoints da API
1. GET /api/items:
Retorna todos os itens armazenados em memória.

2. GET /api/items/{id}:
Retorna um item específico pelo seu ID.

3. POST /api/items:
Cria um novo item.

4. PUT /api/items/{id}:
Atualiza um item existente.

5. DELETE /api/items/{id}
Deleta um item específico.


### Explicação do Cache
A API utiliza o MemoryCache para armazenar em cache os resultados das consultas aos itens. O cache ajuda a evitar a busca repetida pela lista de itens, melhorando o desempenho da aplicação.

- Quando um item ou a lista de itens é requisitado pela primeira vez, ele é carregado da "fonte de dados" (a lista na memória) e armazenado no cache.
- Em requisições subsequentes, a API tenta buscar os dados diretamente do cache. Se os dados já estiverem armazenados, a resposta será mais rápida.
- O cache tem um tempo de expiração de 5 minutos. Após esse período, a próxima requisição irá buscar os dados novamente na "fonte de dados" e atualizar o cache.
