# Neo4j Semantic Kernel Starter Kit

Get started coding with Neo4j & OpenAI's GPT-3 API in C# with this starter kit.

this project is built with [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
```shell
dotnet --version
```

Install packages from root dir
```shell
dotnet restore .
```

Register you OpenAI API key & Neo4j password with [User secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0&tabs=linux).
```shell
cd <PROJECT_ROOT>/CreateKnowledgeGraph
dotnet user-secrets set "OpenAI:ApiKey" "key here"
dotnet user-secrets set "Neo4j:Password" "neo4j password here"
```
this will be read a used from [Config.cs](./CreateKnowledgeGraph/Config.cs) & in [example notebook](./example.ipynb)
this will register your API key & Password for usage with secret id: `2a78a9a7-426d-457c-b4d0-42438d70fa71` 
(this can be seen in both [.csproj](./CreateKnowledgeGraph/CreateKnowledgeGraph.csproj) & [example notebook](./example.ipynb))
