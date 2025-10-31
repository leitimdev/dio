FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ApiGateway/DesafioTecnico.ApiGateway/DesafioTecnico.ApiGateway.csproj", "ApiGateway/DesafioTecnico.ApiGateway/"]
COPY ["Shared/DesafioTecnico.Shared/DesafioTecnico.Shared.csproj", "Shared/DesafioTecnico.Shared/"]
RUN dotnet restore "ApiGateway/DesafioTecnico.ApiGateway/DesafioTecnico.ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway/DesafioTecnico.ApiGateway"
RUN dotnet build "DesafioTecnico.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioTecnico.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioTecnico.ApiGateway.dll"]