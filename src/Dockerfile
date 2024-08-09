FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PhotoCarousel.csproj", "."]
RUN dotnet restore "./PhotoCarousel.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PhotoCarousel.csproj" -c Release -o /app/build
 
FROM build AS publish
RUN dotnet publish "PhotoCarousel.csproj" -c Release -o /app/publish /p:UseAppHost=false
 
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhotoCarousel.dll"]