#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/FinancialManager.Web/Server/FinancialManager.Web.Server.csproj", "src/FinancialManager.Web/Server/"]
COPY ["src/FinancialManager.Web/Shared/FinancialManager.Web.Shared.csproj", "src/FinancialManager.Web/Shared/"]
COPY ["src/FinancialManager.Web/Client/FinancialManager.Web.Client.csproj", "src/FinancialManager.Web/Client/"]
COPY ["src/Infra/FinancialManager.Infra/FinancialManager.Infra.CrossCutting.csproj", "src/Infra/FinancialManager.Infra/"]
RUN dotnet restore "src/FinancialManager.Web/Server/FinancialManager.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/FinancialManager.Web/Server"
RUN dotnet build "FinancialManager.Web.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialManager.Web.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialManager.Web.Server.dll"]