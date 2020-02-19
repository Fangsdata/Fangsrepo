FROM mcr.microsoft.com/dotnet/core/sdk:3.1

LABEL maintainer="Stefán Örn Hrafnsson	stefanorn92@gmail.com"

env ASPNETCORE_URLS=http://+:5000

COPY . /app
WORKDIR /app
RUN dotnet restore
RUN dotnet build

ENTRYPOINT ["dotnet", "run"]
