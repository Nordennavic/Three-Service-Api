#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["erthsobes-db/erthsobes-db.csproj", "erthsobes-db/"]
RUN dotnet restore "erthsobes-db/erthsobes-db.csproj"
COPY . .
WORKDIR "/src/erthsobes-db"
RUN dotnet build "erthsobes-db.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "erthsobes-db.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "erthsobes-db.dll"]