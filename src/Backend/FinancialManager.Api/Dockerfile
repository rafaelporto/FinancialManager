#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Backend/FinancialManager.Api/FinancialManager.Api.csproj", "src/Backend/FinancialManager.Api/"]
RUN dotnet restore "src/Backend/FinancialManager.Api/FinancialManager.Api.csproj"
COPY . .
WORKDIR "/src/src/Backend/FinancialManager.Api"
RUN dotnet build "FinancialManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialManager.Api.dll"]