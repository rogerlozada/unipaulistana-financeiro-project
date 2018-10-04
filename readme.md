# Workflow

Esse sistema foi desenvolvido como um projeto de faculdade, o objetivo do sistema é gerenciar o fluxo de trabalho de 1 ou mais departamento de uma empresa.

# referências

-> instalação do nugget https://www.nuget.org/downloads

-> exemplo de comandos nugget https://docs.microsoft.com/pt-br/nuget/tools/nuget-exe-cli-reference

-> site que contém os pacotes nugget -> https://www.nuget.org/

# Especificações do sistema

O sistema foi desenvolvido com ASP.NET Core 2.1 e SQL Server 2017.

O sistema funciona em máquinas com SO Windows, Linux e MacOS.

O sistema foi desenvolvido utilizando uma máquina Linux Ubuntu Desktop versão 18.

O sistema foi desenvolvido com a ferramenta VSCode, mas você pode utilizar qualquer outra ferramenta de criação de arquivos e pode também usar o Visual Studio.

# Abaixo segue a relação de comandos utilizados para criar a aplicação

dotnet new sln -n unipaulistana.sac -> cria uma solução

dotnet new razor -n unipaulistana.web -> cria um projeto do tipo razor page

dotnet new classlib -n unipaulistana.model -> criação da dll de modelo

dotnet new classlib -n unipaulistana.data -> criação do projeto de data

dotnet new classlib -n unipaulistana.services -> criação do projeto de serviços

dotnet sln unipaulistana.sac.sln add ./unipaulistana.web/unipaulistana.web.csproj -> adicionando projeto a solução

dotnet sln unipaulistana.sac.sln add ./unipaulistana.model/unipaulistana.model.csproj -> adicionar projeto a solução

dotnet sln unipaulistana.sac.sln add ./unipaulistana.data/unipaulistana.data.csproj -> adicionar projeto a solução

dotnet sln unipaulistana.sac.sln add ./unipaulistana.services/unipaulistana.services.csproj -> adicionar projeto a solução

dotnet add unipaulistana.web/unipaulistana.web.csproj reference unipaulistana.model/unipaulistana.model.csproj -> adiciona o projeto model na camada web

dotnet add unipaulistana.data/unipaulistana.data.csproj reference unipaulistana.model/unipaulistana.model.csproj -> adiciona o projeto model na camada data

dotnet add unipaulistana.services/unipaulistana.services.csproj reference unipaulistana.model/unipaulistana.model.csproj -> adiciona o projeto model na camada service

dotnet add unipaulistana.web/unipaulistana.web.csproj reference unipaulistana.data/unipaulistana.data.csproj -> adiciona o projeto data na camada web

dotnet add unipaulistana.web/unipaulistana.web.csproj reference unipaulistana.services/unipaulistana.services.csproj -> adiciona o projeto service na camada web

dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design --version 2.2.0-preview1-35029 -> adicionando scafolding ao projeto

dotnet add package SqlConnection --version 1.0.2 -> adicionar na camada repository














