# build
dotnet build -c Release

# publish
dotnet publish -c Release --no-build -o ./deploy

# remove pdb files
rm ./deploy/*.pdb

# build docker images
docker build -t runtime_demo_5005 .

# run it up
docker run --name my_runtime_demo_5006 -d -p 5006:5005 runtime_demo_5005
