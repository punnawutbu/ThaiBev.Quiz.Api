FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ThaiBev.Quiz.Api/ThaiBev.Quiz.Api.csproj", "ThaiBev.Quiz.Api/"]
COPY ["ThaiBev.Quiz.Api.Shared/ThaiBev.Quiz.Api.Shared.csproj", "ThaiBev.Quiz.Api.Shared/"]
COPY ["ThaiBev.Quiz.Api.Tests/ThaiBev.Quiz.Api.Tests.csproj", "ThaiBev.Quiz.Api.Tests/"]

RUN dotnet restore "ThaiBev.Quiz.Api/ThaiBev.Quiz.Api.csproj"

COPY . .
WORKDIR "/src/ThaiBev.Quiz.Api"
RUN dotnet publish "ThaiBev.Quiz.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 21001

ENTRYPOINT ["dotnet", "ThaiBev.Quiz.Api.dll"]