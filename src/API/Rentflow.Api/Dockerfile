FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/API/Rentflow.Api/Rentflow.Api.csproj", "src/API/Rentflow.Api/"]
COPY ["src/Modules/Fleet/RentFlow.Modules.Fleet.Infrastructure/RentFlow.Modules.Fleet.Infrastructure.csproj", "src/Modules/Fleet/RentFlow.Modules.Fleet.Infrastructure/"]
COPY ["src/Modules/Fleet/RentFlow.Modules.Fleet.Application/RentFlow.Modules.Fleet.Application.csproj", "src/Modules/Fleet/RentFlow.Modules.Fleet.Application/"]
COPY ["src/Common/RentFlow.Common.Application/RentFlow.Common.Application.csproj", "src/Common/RentFlow.Common.Application/"]
COPY ["src/Common/RentFlow.Common.Domain/RentFlow.Common.Domain.csproj", "src/Common/RentFlow.Common.Domain/"]
COPY ["src/Modules/Fleet/RentFlow.Modules.Fleet.Domain/RentFlow.Modules.Fleet.Domain.csproj", "src/Modules/Fleet/RentFlow.Modules.Fleet.Domain/"]
COPY ["src/Modules/Fleet/RentFlow.Modules.Fleet.Presentation/RentFlow.Modules.Fleet.Presentation.csproj", "src/Modules/Fleet/RentFlow.Modules.Fleet.Presentation/"]
COPY ["src/Common/RentFlow.Common.Presentation/RentFlow.Common.Presentation.csproj", "src/Common/RentFlow.Common.Presentation/"]
COPY ["src/Common/Rentflow.Common.Infrastructure/Rentflow.Common.Infrastructure.csproj", "src/Common/Rentflow.Common.Infrastructure/"]
COPY ["src/Modules/Identity/RentFlow.Modules.Identity.Infrastructure/RentFlow.Modules.Identity.Infrastructure.csproj", "src/Modules/Identity/RentFlow.Modules.Identity.Infrastructure/"]
COPY ["src/Modules/Identity/RentFlow.Modules.Identity.Application/RentFlow.Modules.Identity.Application.csproj", "src/Modules/Identity/RentFlow.Modules.Identity.Application/"]
COPY ["src/Modules/Identity/RentFlow.Modules.Identity.Domain/RentFlow.Modules.Identity.Domain.csproj", "src/Modules/Identity/RentFlow.Modules.Identity.Domain/"]
COPY ["src/Modules/Identity/RentFlow.Modules.Identity.Presentation/RentFlow.Modules.Identity.Presentation.csproj", "src/Modules/Identity/RentFlow.Modules.Identity.Presentation/"]
COPY ["src/Modules/Rentals/RentFlow.Modules.Rentals.Infrastructure/RentFlow.Modules.Rentals.Infrastructure.csproj", "src/Modules/Rentals/RentFlow.Modules.Rentals.Infrastructure/"]
COPY ["src/Modules/Rentals/RentFlow.Modules.Rentals.Application/RentFlow.Modules.Rentals.Application.csproj", "src/Modules/Rentals/RentFlow.Modules.Rentals.Application/"]
COPY ["src/Modules/Rentals/RentFlow.Modules.Rentals.Domain/RentFlow.Modules.Rentals.Domain.csproj", "src/Modules/Rentals/RentFlow.Modules.Rentals.Domain/"]
COPY ["src/Modules/Rentals/RentFlow.Modules.Rentals.Presentation/RentFlow.Modules.Rentals.Presentation.csproj", "src/Modules/Rentals/RentFlow.Modules.Rentals.Presentation/"]
RUN dotnet restore "src/API/Rentflow.Api/Rentflow.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Rentflow.Api"
RUN dotnet build "./Rentflow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Rentflow.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rentflow.Api.dll"]
