# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files first (for caching restore layer)
COPY ["EdTech.WebApi/EdTech.WebApi.csproj", "EdTech.WebApi/"]
COPY ["EdTech.Application/EdTech.Application.csproj", "EdTech.Application/"]
COPY ["EdTech.Core/EdTech.Core.csproj", "EdTech.Core/"]
COPY ["EdTech.Infrastructure/EdTech.Infrastructure.csproj", "EdTech.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "EdTech.WebApi/EdTech.WebApi.csproj"

# Copy everything else
COPY . .

# Build
WORKDIR "/src/EdTech.WebApi"
RUN dotnet build "EdTech.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "EdTech.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EdTech.WebApi.dll"]
