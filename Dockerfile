FROM microsoft/aspnetcore:2.1-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.1-nanoserver-1709 AS build
WORKDIR /src
COPY Doctrina.Web/Doctrina.Web.csproj ..\build\Doctrina.Web/
RUN dotnet restore Doctrina.Web/Doctrina.Web.csproj
COPY . .
WORKDIR /src/Doctrina.Web
RUN dotnet build Doctrina.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Doctrina.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Doctrina.Web.dll"]
