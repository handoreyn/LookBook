FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src

COPY . .
RUN ["ls", "-la"]
RUN dotnet restore /src/Bakery.Services/Bakery.Member/Bakery.Member.Api/Bakery.Member.Api.csproj
RUN dotnet build /src/Bakery.Services/Bakery.Member/Bakery.Member.Api/Bakery.Member.Api.csproj

FROM build AS publish
RUN dotnet publish /src/Bakery.Services/Bakery.Member/Bakery.Member.Api/Bakery.Member.Api.csproj -o /publish -c Release

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as final
WORKDIR /app
COPY --from=publish /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Bakery.Member.Api.dll"]