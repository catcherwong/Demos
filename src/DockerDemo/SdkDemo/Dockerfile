FROM microsoft/dotnet:2.1-sdk

ENV ASPNETCORE_URLS http://*:5011

EXPOSE 5011

WORKDIR /app

COPY ./deploy/* ./

ENTRYPOINT ["dotnet", "SdkDemo.dll"]
