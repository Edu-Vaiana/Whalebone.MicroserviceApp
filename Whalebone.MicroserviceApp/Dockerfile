# Use the official ASP.NET Core runtime as a base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Add CA certificates - Needed because my machine uses Zscaler (for dev only)
COPY ZscalerRootCA.crt .
ADD ZscalerRootCA.crt /usr/local/share/ca-certificates/ZscalerRootCA.crt
ADD ZscalerRootCA.crt /etc/ssl/certs/ZscalerRootCA.crt
RUN chmod 644 /usr/local/share/ca-certificates/ZscalerRootCA.crt && update-ca-certificates

# Install/update CA certificates
RUN apt-get update && \
    apt-get install -y --reinstall ca-certificates && \
    update-ca-certificates

# Test SSL with curl
#RUN apt-get update && apt-get install -y curl && curl -Iv https://api.nuget.org/v3/index.json

# Copy nuget.config to ignore SSL errors (for dev only)
#COPY nuget.config ./

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Whalebone.MicroserviceApp.dll"]
