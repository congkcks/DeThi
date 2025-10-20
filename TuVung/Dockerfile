# ===========================
# 1️⃣ Build stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file và restore dependencies
COPY TuVung/TuVung.csproj TuVung/
RUN dotnet restore TuVung/TuVung.csproj

# Copy toàn bộ source code
COPY . .

# Build và publish ra thư mục /app
RUN dotnet publish TuVung/TuVung.csproj -c Release -o /app/publish /p:UseAppHost=false

# ===========================
# 2️⃣ Runtime stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Mặc định Render mở cổng 8080
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "TuVung.dll"]
