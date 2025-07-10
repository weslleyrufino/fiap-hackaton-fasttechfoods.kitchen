# Etapa 1 - build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia apenas os arquivos de projeto para restaurar as dependências
COPY ["GestorContatos.API/GestorContatos.API.csproj", "GestorContatos.API/"]
RUN dotnet restore "GestorContatos.API/GestorContatos.API.csproj"

# Copia o restante do código-fonte
COPY . .

# Publica o projeto
WORKDIR "/src/GestorContatos.API"
RUN dotnet publish -c Release -o /app/publish

# Etapa 2 - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GestorContatos.API.dll"]
