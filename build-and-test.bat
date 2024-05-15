cd FixFlow.Server

dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet restore
dotnet build -c Release .\FixFlow.Server.csproj
swagger tofile --output .\swagger.json .\bin\Release\net8.0\FixFlow.Server.dll v1

echo "React build"

cd ..\fixflow.client\src

npm install -g npm
npm install -g react
npm install
openapi -i ..\..\FixFlow.Server\swagger.json -o FlowApi -c axios
npm run build
npm run test

cd ..\..