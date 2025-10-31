FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Microservices/VendasService/DesafioTecnico.VendasService/DesafioTecnico.VendasService.csproj", "Microservices/VendasService/DesafioTecnico.VendasService/"]
COPY ["Shared/DesafioTecnico.Shared/DesafioTecnico.Shared.csproj", "Shared/DesafioTecnico.Shared/"]
RUN dotnet restore "Microservices/VendasService/DesafioTecnico.VendasService/DesafioTecnico.VendasService.csproj"
COPY . .
WORKDIR "/src/Microservices/VendasService/DesafioTecnico.VendasService"
RUN dotnet build "DesafioTecnico.VendasService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioTecnico.VendasService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioTecnico.VendasService.dll"]