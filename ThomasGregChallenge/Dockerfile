#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 82
EXPOSE 445

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ThomasGregChallenge/ThomasGregChallenge.csproj", "ThomasGregChallenge/"]
COPY ["ThomasGregChallenge.Infraestrutura/ThomasGregChallenge.Infrastructure.csproj", "ThomasGregChallenge.Infraestrutura/"]
COPY ["ThomasGregChallenge.Domain/ThomasGregChallenge.Domain.csproj", "ThomasGregChallenge.Dominio/"]
COPY ["ThomasGregChallenge.Application/ThomasGregChallenge.Application.csproj", "ThomasGregChallenge.Services/"]
RUN dotnet restore "./ThomasGregChallenge/./ThomasGregChallenge.csproj"
COPY . .
WORKDIR "/src/ThomasGregChallenge"
RUN dotnet build "./ThomasGregChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "./ThomasGregChallenge.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThomasGregChallenge.dll"]