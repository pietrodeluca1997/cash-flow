#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["source/services/transactions/CF.Transactions.API/CF.Transactions.API.csproj", "source/services/transactions/CF.Transactions.API/"]
COPY ["source/building blocks/CF.Core/CF.Core.csproj", "source/building blocks/CF.Core/"]
COPY ["source/building blocks/CF.CustomMediator/CF.CustomMediator.csproj", "source/building blocks/CF.CustomMediator/"]
RUN dotnet restore "source/services/transactions/CF.Transactions.API/CF.Transactions.API.csproj"
COPY . .
WORKDIR "/src/source/services/transactions/CF.Transactions.API"
RUN dotnet build "CF.Transactions.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CF.Transactions.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CF.Transactions.API.dll"]