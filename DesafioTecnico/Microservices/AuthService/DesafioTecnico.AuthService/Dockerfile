FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Microservices/AuthService/DesafioTecnico.AuthService/DesafioTecnico.AuthService.csproj", "Microservices/AuthService/DesafioTecnico.AuthService/"]
COPY ["Shared/DesafioTecnico.Shared/DesafioTecnico.Shared.csproj", "Shared/DesafioTecnico.Shared/"]
RUN dotnet restore "Microservices/AuthService/DesafioTecnico.AuthService/DesafioTecnico.AuthService.csproj"
COPY . .
WORKDIR "/src/Microservices/AuthService/DesafioTecnico.AuthService"
RUN dotnet build "DesafioTecnico.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioTecnico.AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioTecnico.AuthService.dll"]