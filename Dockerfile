# ---
# First stage (build)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy solution as distinct layer
COPY WomWebDemo.sln .
COPY WomWebDemo/WomWebDemo.csproj ./WomWebDemo/
COPY WomWebDemo/WomWebDemo.csproj ./WomWebDemo/
RUN dotnet restore

# Copy everything else and build
COPY WomWebDemo/. ./WomWebDemo/
WORKDIR /app/WomWebDemo
RUN dotnet publish -c Release -o out

# ---
# Second stage (execution)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /app
COPY --from=build /app/WomWebDemo/out ./

# Fix console logging
ENV Logging__Console__FormatterName=

# Run on localhost:8080
ENV ASPNETCORE_URLS http://+:8080
EXPOSE 8080

# Drop privileges
USER 1000

ENTRYPOINT ["dotnet", "WomWebDemo.dll"]
