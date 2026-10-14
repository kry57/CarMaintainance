FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore CarMaintainance.API/CarMaintainance.API.csproj
RUN dotnet publish CarMaintainance.API/CarMaintainance.API.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 10000
CMD ASPNETCORE_URLS=http://0.0.0.0:$PORT dotnet CarMaintainance.API.dll
