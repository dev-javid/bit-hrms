#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs


FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/src/Application/Application.csproj", "./Application/"]
COPY ["/src/Domain/Domain.csproj", "./Domain/"]
COPY ["/src/Infrastructure/Infrastructure.csproj", "./Infrastructure/"]
COPY ["/src/Presentation/Presentation.csproj", "./Presentation/"]
COPY ["/src/Database/Database.csproj", "./Database/"]
COPY ["/src/react.client/react.client.esproj", "./react.client/"]

RUN dotnet restore "./Presentation/Presentation.csproj"

COPY src .

WORKDIR "/src/Presentation"

RUN ls -la ./

RUN dotnet build "./Presentation.csproj" -c $BUILD_CONFIGURATION -o /src/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Presentation.csproj" -c $BUILD_CONFIGURATION -o /src/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /src
COPY --from=publish /src/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
