# ./src/mzu.libs.rollercoaster.examples.webapi/Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["./mzu.libs.rollercoaster.examples.webapi/mzu.libs.rollercoaster.examples.webapi.csproj", "mzu.libs.rollercoaster.examples.webapi/"]
COPY ["./mzu.libs.rollercoaster/Mzu.Libs.Rollercoaster.csproj", "mzu.libs.rollercoaster/"]
RUN dotnet restore "mzu.libs.rollercoaster.examples.webapi/mzu.libs.rollercoaster.examples.webapi.csproj"
COPY . .
WORKDIR "/src/mzu.libs.rollercoaster.examples.webapi"
RUN dotnet build "mzu.libs.rollercoaster.examples.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "mzu.libs.rollercoaster.examples.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mzu.libs.rollercoaster.examples.webapi.dll"]