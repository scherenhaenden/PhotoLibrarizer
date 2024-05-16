FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PhotoLibrarizerCliDotnet/PhotoLibrarizerCliDotnet.csproj", "PhotoLibrarizerCliDotnet/"]
RUN dotnet restore "PhotoLibrarizerCliDotnet/PhotoLibrarizerCliDotnet.csproj"
COPY . .
WORKDIR "/src/PhotoLibrarizerCliDotnet"
RUN dotnet build "PhotoLibrarizerCliDotnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhotoLibrarizerCliDotnet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoLibrarizerCliDotnet.dll"]
