FROM mcr.microsoft.com/dotnet/sdk:5.0 AS publish
WORKDIR /src
COPY . .
RUN dotnet restore Reports.Notifications.sln -s http://nuget.platius.lan:81/nuget/Default/

#test
# RUN dotnet test -c Release
#build
RUN dotnet publish src/Reports.Notifications.Host/Reports.Notifications.Host.csproj -c Release -r linux-x64 -o /app


FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /app .
EXPOSE 9229/tcp
ENTRYPOINT ["./Reports.Notifications.Host"]