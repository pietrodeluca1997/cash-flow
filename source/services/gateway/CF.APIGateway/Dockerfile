#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["source/services/gateway/CF.APIGateway/CF.APIGateway.csproj", "source/services/gateway/CF.APIGateway/"]
RUN dotnet restore "source/services/gateway/CF.APIGateway/CF.APIGateway.csproj"
COPY . .
WORKDIR "/src/source/services/gateway/CF.APIGateway"
RUN dotnet build "CF.APIGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CF.APIGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CF.APIGateway.dll"]