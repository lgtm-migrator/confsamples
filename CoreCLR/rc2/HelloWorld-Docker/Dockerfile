FROM microsoft/dotnet-preview:latest
EXPOSE 5003/tcp

COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]

RUN dotnet build --configuration release

ENV ASPNETCORE_ENVIRONMENT production 

ENTRYPOINT dotnet run --configuration release --server.urls http://*:5003
