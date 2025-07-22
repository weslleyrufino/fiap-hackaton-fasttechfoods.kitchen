# FastTechFoods.Kitchen.Worker/Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["FastTechFoods.Kitchen.Worker/FastTechFoods.Kitchen.Worker.csproj", "FastTechFoods.Kitchen.Worker/"]
RUN dotnet restore "FastTechFoods.Kitchen.Worker/FastTechFoods.Kitchen.Worker.csproj"

COPY . .
WORKDIR "/src/FastTechFoods.Kitchen.Worker"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FastTechFoods.Kitchen.Worker.dll"]
