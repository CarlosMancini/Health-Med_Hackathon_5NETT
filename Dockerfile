FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../Health&Med_Hackathon_5NETT.sln . 

COPY ./HealthMed/HealthMed.csproj ./HealthMed/
COPY ./Core/Core.csproj ./Core/
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/

RUN dotnet restore

COPY . .
WORKDIR /src/HealthMed
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "HealthMed.dll"]