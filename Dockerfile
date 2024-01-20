FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5276

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HxcCommon/HxcCommon.csproj", "HxcCommon/"]
RUN dotnet restore "HxcCommon/HxcCommon.csproj"

COPY ["HxcApi/HxcApi/HxcApi.csproj", "HxcApi/HxcApi/"]
RUN dotnet restore "HxcApi/HxcApi/HxcApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "HxcCommon/HxcCommon.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet build "HxcApi/HxcApi/HxcApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HxcApi/HxcApi/HxcApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false /p:PublishAot=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HxcApi.dll", "--urls", "http://0.0.0.0:5276"]

