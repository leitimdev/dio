FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Microservices/EstoqueService/DesafioTecnico.EstoqueService/DesafioTecnico.EstoqueService.csproj", "Microservices/EstoqueService/DesafioTecnico.EstoqueService/"]
COPY ["Shared/DesafioTecnico.Shared/DesafioTecnico.Shared.csproj", "Shared/DesafioTecnico.Shared/"]
RUN dotnet restore "Microservices/EstoqueService/DesafioTecnico.EstoqueService/DesafioTecnico.EstoqueService.csproj"
COPY . .
WORKDIR "/src/Microservices/EstoqueService/DesafioTecnico.EstoqueService"
RUN dotnet build "DesafioTecnico.EstoqueService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioTecnico.EstoqueService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioTecnico.EstoqueService.dll"]