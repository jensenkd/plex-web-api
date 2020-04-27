FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY src/*.csproj .
RUN dotnet restore 

COPY src/. ./
RUN dotnet publish Plex.Web.Api.csproj -c Release -o out

# Build the image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Plex.Web.Api.dll"]
