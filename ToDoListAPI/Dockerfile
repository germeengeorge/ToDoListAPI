# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
RUN adduser --disabled-password --gecos '' appuser
USER appuser
WORKDIR /app
EXPOSE 8080 8081

# Build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy only the .csproj file
COPY ToDoListAPI.csproj ./
RUN dotnet restore "./ToDoListAPI.csproj"

# Copy the rest of the source code
COPY . ./

# Build the app
RUN dotnet build "ToDoListAPI.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "ToDoListAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image to run the app
FROM base AS final
ENV ASPNETCORE_ENVIRONMENT=Production
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoListAPI.dll"]
