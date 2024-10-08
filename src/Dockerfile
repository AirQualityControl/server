# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

EXPOSE 8080

WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# Copy csproj and restore as distinct layers
COPY ["AirSnitch.API/AirSnitch.API.csproj", "AirSnitch.API/"]
COPY ["AirSnitch.Di/AirSnitch.Di.csproj", "AirSnitch.Di/"]
COPY ["AirSnitch.Domain/AirSnitch.Domain.csproj", "AirSnitch.Domain/"]
COPY ["AirSnitch.Infrastructure/AirSnitch.Infrastructure.csproj", "AirSnitch.Infrastructure/"]
COPY ["AirSnitch.Infrastructure.Abstract/AirSnitch.Infrastructure.Abstract.csproj", "AirSnitch.Infrastructure.Abstract/"]

COPY . .
WORKDIR "/src/AirSnitch.API"
RUN dotnet build "AirSnitch.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirSnitch.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirSnitch.API.dll"]
