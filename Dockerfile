FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Task API.csproj", "./"]
RUN dotnet restore "Task API.csproj"
WORKDIR "/src/."
COPY . .
WORKDIR "/src"
RUN dotnet build "
