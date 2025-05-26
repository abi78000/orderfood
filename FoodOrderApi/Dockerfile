FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything to the container's /src folder
COPY . .

# Restore dependencies
RUN dotnet restore "FoodOrderApi.csproj"

# Build the project in Release configuration
RUN dotnet build "FoodOrderApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodOrderApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodOrderApi.dll"]
