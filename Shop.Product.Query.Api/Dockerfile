FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.Product.Query.Api/Shop.Product.Query.Api.csproj", "Shop.Product.Query.Api/"]
RUN dotnet restore "Shop.Product.Query.Api/Shop.Product.Query.Api.csproj"
COPY . .
WORKDIR "/src/Shop.Product.Query.Api"
RUN dotnet build "Shop.Product.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Product.Query.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Product.Query.Api.dll"]
