FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dockerization/Dockerization.csproj", "Dockerization/"]
RUN dotnet restore "Dockerization/Dockerization.csproj"
COPY . .
WORKDIR "/src/Dockerization"
RUN dotnet build "Dockerization.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dockerization.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dockerization.dll"]