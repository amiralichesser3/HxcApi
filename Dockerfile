FROM mcr.microsoft.com/dotnet/nightly/runtime-deps:8.0-jammy-chiseled-extra AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5276
FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0-jammy-aot AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HxcCommon/HxcCommon.csproj", "HxcCommon/"]
RUN dotnet restore -r linux-x64 "HxcCommon/HxcCommon.csproj"

COPY ["HxcApi/HxcApi/HxcApi.csproj", "HxcApi/HxcApi/"]
RUN dotnet restore -r linux-x64 "HxcApi/HxcApi/HxcApi.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build -r linux-x64 "HxcCommon/HxcCommon.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet build -r linux-x64 "HxcApi/HxcApi/HxcApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "HxcApi/HxcApi/HxcApi.csproj"  --no-restore -r linux-x64 -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true /p:PublishAot=true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./HxcApi"]
