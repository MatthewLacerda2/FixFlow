# Use the latest .NET SDK as the build image
FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /app

# Copy only the necessary files for restoring dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the entire application and build it
COPY . .
RUN dotnet publish -c Release -o out

# Use the ASP.NET Core runtime as the runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:latest AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose the port your application will run on
EXPOSE 5105
ENV ASPNETCORE_URLS=http://+:5105

# Define the entry point for the application
ENTRYPOINT ["dotnet", "FixFlow.Server.dll"]
