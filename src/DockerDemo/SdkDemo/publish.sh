# build the csproj
dotnet build -c Release

# publish to deploy folder
dotnet publish -c Release --no-build -o ./deploy

# rmove pdb files
rm ./deploy/*.pdb

# build a docker image
docker build -t demoapp_sdk_5011 .

# run it up
docker run --name my_demoapp_sdk_5011 -d -p 5012:5011 demoapp_sdk_5011
