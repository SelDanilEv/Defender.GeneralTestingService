docker rm -f LocalGeneralTestingService
docker build . -t local-general-testing-service && ^
docker run -d --name LocalGeneralTestingService -p 47059:80 ^
--env-file ./../../secrets/secrets.local.list ^
-e ASPNETCORE_ENVIRONMENT=DockerLocal ^
-it local-general-testing-service
echo finish local-general-testing-service
docker image prune -f
pause
