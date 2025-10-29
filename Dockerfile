# Etapa 1: Construcción
# Usamos la imagen oficial de SDK de .NET 8 para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo .csproj y restaura las dependencias
COPY ["pokemon_backend.csproj", "./"]
RUN dotnet restore "./pokemon_backend.csproj"

# Copia todo el resto del código fuente
COPY . .
WORKDIR "/src/."
# Publica la aplicación en modo Release
RUN dotnet publish "pokemon_backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
# Usamos una imagen ligera de ASP.NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Expone el puerto 8080 (Render espera este puerto en los contenedores)
# La variable de entorno ASPNETCORE_URLS le dice a .NET que escuche en ese puerto.
ENV ASPNETCORE_URLS=http://+:8080

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "pokemon_backend.dll"]