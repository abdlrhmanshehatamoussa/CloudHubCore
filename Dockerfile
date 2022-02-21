FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CloudHub.API/CloudHub.API.csproj", "CloudHub.API/"]
COPY ["CloudHub.Domain/CloudHub.Domain.csproj", "CloudHub.Domain/"]
COPY ["CloudHub.Utils/CloudHub.Utils.csproj", "CloudHub.Utils/"]
COPY ["CloudHub.Infra/CloudHub.Infra.csproj", "CloudHub.Infra/"]
COPY ["CloudHub.ServiceImp/CloudHub.ServiceImp.csproj", "CloudHub.ServiceImp/"]
RUN dotnet restore "CloudHub.API/CloudHub.API.csproj"
COPY . .
WORKDIR "/src/CloudHub.API"
RUN dotnet build "CloudHub.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudHub.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudHub.API.dll"]