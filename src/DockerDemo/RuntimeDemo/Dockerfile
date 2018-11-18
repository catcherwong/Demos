FROM microsoft/dotnet:2.1-aspnetcore-runtime

WORKDIR /app

COPY ./deploy/* ./

EXPOSE 5005

ENTRYPOINT ["dotnet", "RuntimeDemo.dll", "--urls", "http://*:5005"]
