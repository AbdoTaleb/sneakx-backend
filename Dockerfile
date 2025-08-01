FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /src/out ./

ENV ASPNETCORE_URLS=http://+:10000

ENTRYPOINT ["dotnet", "SneakX.API.dll"]
