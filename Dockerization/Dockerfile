#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . /app
WORKDIR /app
RUN dotnet restore "Dockerization.csproj"
RUN dotnet build "Dockerization.csproj" -c Release -o /app/build
RUN dotnet publish "Dockerization.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Dockerization.dll"]