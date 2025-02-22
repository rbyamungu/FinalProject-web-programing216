# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the project file first to take advantage of Docker layer caching
COPY ["HalfAndHalf.csproj", "./"]
RUN dotnet restore --no-cache

# Copy the rest of the source code
COPY . .

# Build and publish
RUN dotnet publish -c Release -o /app/publish \
    --no-restore \
    # Remove unnecessary files
    /p:DebugType=None \
    /p:DebugSymbols=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /app/publish .

# Create a non-root user for security
RUN addgroup -S appgroup && adduser -S appuser -G appgroup
USER appuser

# Configure ASP.NET Core
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

ENTRYPOINT ["dotnet", "HalfAndHalf.dll"]