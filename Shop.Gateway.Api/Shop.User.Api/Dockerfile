FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Shop.User.Api/Shop.User.Api.csproj", "Shop.User.Api/"]
RUN dotnet restore "Shop.User.Api/Shop.User.Api.csproj"
COPY . .
WORKDIR "/src/Shop.User.Api"
RUN dotnet build "Shop.User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.User.Api.dll"]
