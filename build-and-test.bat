@echo off

REM Dotnet Build
cd /d "/FixFlow.Server"
dotnet restore
dotnet tool install -g Swashbuckle.AspNetCore.Cli
dotnet build -c Release .\FixFlow.Server.csproj
swagger tofile --output .\swagger.json .\bin\Release\net8.0\FixFlow.Server.dll v1

REM Dotnet test
cd /d "/FixFlow.Tests"
dotnet restore
dotnet test

REM React build
cd /d "/fixflow.client/src"
npm install
openapi -i ..\..\FixFlow.Server\swagger.json -o FlowApi -c axios
npm install openapi-typescript-codegen -g
npm run build
