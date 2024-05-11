FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NSIProject/NSIProject.Api.csproj", "NSIProject/"]
COPY ["NSIProject.Infrastructure/NSIProject.Infrastructure.csproj", "NSIProject.Infrastructure/"]
COPY ["NSIProject.Application/NSIProject.Application.csproj", "NSIProject.Application/"]
COPY ["NSIProject.Domain/NSIProject.Domain.csproj", "NSIProject.Domain/"]
RUN dotnet restore "NSIProject/NSIProject.Api.csproj"
COPY . .
WORKDIR "/src/NSIProject"
RUN dotnet build "NSIProject.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NSIProject.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSIProject.Api.dll"]
