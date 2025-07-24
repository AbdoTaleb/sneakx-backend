# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore separately for better caching
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the code
COPY . . 

# Build the project
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/out ./

# Configure app to listen on port 10000
ENV ASPNETCORE_URLS=http://+:10000

# Entry point
ENTRYPOINT ["dotnet", "SneakX.API.dll"]
