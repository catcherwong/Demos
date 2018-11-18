# build a docker image
docker build -t catcherwong/runtime_demo_6005 .

# run it up
docker run --name my_runtime_demo_6006 -d -p 6006:6005 catcherwong/runtime_demo_6005
