FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.Product.Api/Shop.Product.Api.csproj", "Shop.Product.Api/"]
RUN dotnet restore "Shop.Product.Api/Shop.Product.Api.csproj"
COPY . .
WORKDIR "/src/Shop.Product.Api"
RUN dotnet build "Shop.Product.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.Product.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.Product.Api.dll"]
