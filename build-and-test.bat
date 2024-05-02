cd FixFlow.Server

dotnet build -c Release .\FixFlow.Server.csproj
swagger tofile --output .\swagger.json .\bin\Release\net8.0\FixFlow.Server.dll v1

echo "React build"

cd ..\fixflow.client\src

openapi -i ..\..\FixFlow.Server\swagger.json -o FlowApi -c axios
npm run build
npm run test

cd ..\..