#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ThomasGregChallenge.UI/ThomasGregChallenge.UI.csproj", "ThomasGregChallenge.UI/"]
RUN dotnet restore "./ThomasGregChallenge.UI/./ThomasGregChallenge.UI.csproj"
COPY . .
WORKDIR "/src/ThomasGregChallenge.UI"
RUN dotnet build "./ThomasGregChallenge.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./ThomasGregChallenge.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThomasGregChallenge.UI.dll"]