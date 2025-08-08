﻿# ========== مرحلة البناء ==========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# ========== مرحلة التشغيل ==========
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# اسمع على البورت 80
EXPOSE 80

# شغّل التطبيق
ENTRYPOINT ["dotnet", "SneakX.API.dll"]
