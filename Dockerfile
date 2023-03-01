FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5068

ENV ASPNETCORE_URLS=http://+:5068

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

#Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ItemApi.csproj", "./"]
RUN dotnet restore "ItemApi.csproj"
COPY . .
RUN dotnet publish "ItemApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

#Final stage copying files from build stage to directory
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ItemApi.dll"] 