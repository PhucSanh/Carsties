FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

COPY Carsties.slnx ./
COPY src/SearchService/SearchService.csproj ./src/SearchService/
COPY src/Contracts/Carsties.Shared.Data/Carsties.Shared.Data.csproj ./src/Contracts/Carsties.Shared.Data/
COPY src/Contracts/Carsties.Shared.Excel/Carsties.Shared.Excel.csproj ./src/Contracts/Carsties.Shared.Excel/
COPY src/Contracts/Carsties.Shared.ExceptionHandler/Carsties.Shared.ExceptionHandler.csproj ./src/Contracts/Carsties.Shared.ExceptionHandler/

RUN dotnet restore ./src/SearchService/SearchService.csproj

COPY . .
RUN dotnet publish ./src/SearchService/SearchService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet","SearchService.dll"]
