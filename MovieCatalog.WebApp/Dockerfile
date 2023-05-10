# Baseado no tutorial: https://www.macoratti.net/20/01/aspncore_dockweb32.htm
# Dados de imagem https://hub.docker.com/_/microsoft-dotnet-aspnet

# Última versão do asp.net core sdk
FROM mcr.microsoft.com/dotnet/sdk AS build-env

WORKDIR /app

# Copiar csproj e restaurar dependencias
COPY *.csproj ./

RUN dotnet restore

# Build da aplicação
COPY . ./

RUN dotnet publish -c Release -o dist

# Build da imagem
FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /app

EXPOSE 80

COPY --from=build-env /app/dist .

ENTRYPOINT ["dotnet", "MovieCatalog.WebApp.dll"]
