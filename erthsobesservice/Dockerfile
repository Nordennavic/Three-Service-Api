#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 6500:6500

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["erthsobesservice/erthsobesservice.csproj", "erthsobesservice/"]
RUN dotnet restore "erthsobesservice/erthsobesservice.csproj"
COPY . .
WORKDIR "/src/erthsobesservice"
RUN dotnet build "erthsobesservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "erthsobesservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "erthsobesservice.dll"]