FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TaskAPI.csproj"
WORKDIR "/src"
RUN dotnet build "TaskAPI.csproj" -c Release -o /app/build
RUN dotnet publish "TaskAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TaskAPI.dll"]
