FROM mcr.microsoft.com/dotnet/core/sdk:3.0
WORKDIR /app
COPY ./src/BeComfy.Services.Employees/bin/Release/netcoreapp3.0 .
ENV ASPNETCORE_URLS http://*:5035
ENV ASPNETCORE_ENVIRONMENT Release
EXPOSE 5035
ENTRYPOINT ["dotnet", "BeComfy.Services.Employees.dll"]