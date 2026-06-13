# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["evaluacion 3/evaluacion 3.csproj", "evaluacion 3/"]
RUN dotnet restore "evaluacion 3/evaluacion 3.csproj"

COPY . .
WORKDIR "/src/evaluacion 3"
RUN dotnet publish "evaluacion 3.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "evaluacion 3.dll"]
