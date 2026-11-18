FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["CmsBase/CmsBase.csproj", "CmsBase/"]
COPY ["CMS.Utility/CMS.Utility.csproj", "CMS.Utility/"]
RUN dotnet restore "CmsBase/CmsBase.csproj"
COPY . .
RUN dotnet publish "CmsBase/CmsBase.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CmsBase.dll"]
