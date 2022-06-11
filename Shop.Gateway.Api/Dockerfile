FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.Gateway.Api/Shop.Gateway.Api.csproj", "Shop.Gateway.Api/"]
RUN dotnet restore "Shop.Gateway.Api/Shop.Gateway.Api.csproj"
COPY . .
WORKDIR "/src/Shop.Gateway.Api"
RUN dotnet build "Shop.Gateway.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Gateway.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Gateway.Api.dll"]
