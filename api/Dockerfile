# Use the official .NET Core SDK image to build the app (with .NET 8 SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj file and restore any dependencies (via `dotnet restore`)
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the app source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET Core Runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the built application from the build container
COPY --from=build /app/out .

# Expose the port the app will run on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "PMSAT.dll"]