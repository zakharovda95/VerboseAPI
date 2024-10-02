FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EndPoints/EndPoints.csproj", "EndPoints/"]
RUN dotnet restore "EndPoints/EndPoints.csproj"
COPY . .
WORKDIR "/src/EndPoints"
RUN dotnet build "EndPoints.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EndPoints.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EndPoints.dll"]
