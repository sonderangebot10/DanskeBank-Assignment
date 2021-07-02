#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build

COPY ["src/DanskeBank.CompaniesApi/DanskeBank.CompaniesApi.csproj", "src/DanskeBank.CompaniesApi/"]
COPY ["src/DanskeBank.Infrastructure/DanskeBank.Infrastructure.csproj", "src/DanskeBank.Infrastructure/"]
COPY ["src/DanskeBank.Application/DanskeBank.Application.csproj", "src/DanskeBank.Application/"]
COPY ["src/DanskeBank.Domain/DanskeBank.Domain.csproj", "src/DanskeBank.Domain/"]

RUN dotnet restore "src/DanskeBank.CompaniesApi/DanskeBank.CompaniesApi.csproj"

COPY . .

WORKDIR "/src/DanskeBank.CompaniesApi"

RUN dotnet build "DanskeBank.CompaniesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DanskeBank.CompaniesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DanskeBank.CompaniesApi.dll"]