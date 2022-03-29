# KanbanApp

### Introdução
Desafio técnico proposto pela equipe da Let's Code.
Foi escolhido desenvolver o backend da aplicação especificada utilizando C# + ASP.NET Core + WebApi.

### Implementação

De acordo com os requisitos passados, o sistema de autenticação (login e senha) foi implementado usando JWT. A requisição deve conter os campos "login" e "senha" conforme especificado para efetuar o login no sistema.
Para efetuar requisições, é necessário configurar o arquivo "appsettings.json". Nele, deve-se substituir os campos "CUSTOM_CHAVE_JWT", "CUSTOM_USUARIO" e "CUSTOM_SENHA" dentro da diretiva "JWT".

Os entrypoints da aplicação foram configurados para a Porta 5000.
Assim, para efetuar o login deve-se fazer uma requisição POST. 

```
(POST)      http://localhost:5000/login/
```

Uma vez que o login for bem sucedido, pode-se efetuar operações CRUD nos cards do kanban. Estas operações seguem os endereços descritos a seguir.

```
(GET)       http://localhost:5000/cards/
(POST)      http://localhost:5000/cards/
(PUT)       http://localhost:5000/cards/{id}
(DELETE)    http://localhost:5000/cards/{id}
```

### Rodando aplicação via terminal
A aplicação desenvolvida para o desafio técnico pode ser executada por meio de um terminal, executando o seguinte comando dentro da pasta raiz do projeto KanbanApp onde encontra-se o arquivo .csproj.

```
dotnet run --project KanbanApp.csproj
```