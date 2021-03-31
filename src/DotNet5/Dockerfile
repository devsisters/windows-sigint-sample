FROM mcr.microsoft.com/dotnet/sdk:5.0-nanoserver-2004 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-nanoserver-2004
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SignalHandlerTestManaged.dll"]
