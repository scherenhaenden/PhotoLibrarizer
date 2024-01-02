#FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
#WORKDIR /src
#COPY ["PhotoLibrarizer.Web.Blazor/", "PhotoLibrarizer.Web.Blazor/"]
#RUN dotnet restore "PhotoLibrarizer.Web.Blazor/PhotoLibrarizer.Web.Blazor.csproj"
#COPY . .
#WORKDIR "/src/PhotoLibrarizer.Web.Blazor"
#RUN dotnet build "PhotoLibrarizer.Web.Blazor.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "PhotoLibrarizer.Web.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "PhotoLibrarizer.Web.Blazor.dll"]

#dotnet build -c Release -o out/app/build
#dotnet publish -c Release -o out/app/publish

# Use a suitable base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

EXPOSE 80
EXPOSE 443

# Create the directory for the application
RUN mkdir /app/out/app/publish -p

# Set the working directory
WORKDIR /app/out/app/publish

# generate certificate
RUN dotnet dev-certs https

# Copy the published application
COPY ./out .

WORKDIR /app/out/app/publish
RUN ls -la
RUN pwd
RUN apt-get -qq clean  
RUN apt-get -qq update
RUN apt-get -qq -y install apt-utils
RUN apt-get -qq -y install dialog
RUN apt-get -qq -y install bash-completion --fix-missing
RUN apt-get -qq -y dist-upgrade
RUN apt-get -qq -y install wget libgdiplus
RUN apt-get -qq -y install sudo libterm-readline-gnu-perl apache2 iputils-ping net-tools
RUN apt-get -qq -y install ssh bash apt-utils apt-transport-https software-properties-common curl supervisor vim apt-utils
RUN dotnet --list-runtimes
RUN dotnet --info

ENTRYPOINT ["dotnet", "PhotoLibrarizer.Web.Blazor.dll"]