#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["interval-recall.API/IntervalRecall.API.csproj", "interval-recall.API/"]
COPY ["interval-recall.BLL/IntervalRecall.BLL.csproj", "interval-recall.BLL/"]
COPY ["interval-recall.DAL/IntervalRecall.DAL.csproj", "interval-recall.DAL/"]
COPY ["interval-recall.Models/IntervalRecall.Models.csproj", "interval-recall.Models/"]
RUN dotnet restore "./interval-recall.API/./IntervalRecall.API.csproj"
COPY . .
WORKDIR "/src/interval-recall.API"
RUN dotnet build "./IntervalRecall.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IntervalRecall.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IntervalRecall.API.dll"]