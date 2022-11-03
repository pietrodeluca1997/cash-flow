#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["source/services/identity/CF.Identity.API/CF.Identity.API.csproj", "source/services/identity/CF.Identity.API/"]
COPY ["source/building blocks/CF.Core/CF.Core.csproj", "source/building blocks/CF.Core/"]
COPY ["source/building blocks/CF.CustomMediator/CF.CustomMediator.csproj", "source/building blocks/CF.CustomMediator/"]
RUN dotnet restore "source/services/identity/CF.Identity.API/CF.Identity.API.csproj"
COPY . .
WORKDIR "/src/source/services/identity/CF.Identity.API"
RUN dotnet build "CF.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CF.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CF.Identity.API.dll"]