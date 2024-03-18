docker rm -f DevGeneralTestingService
docker build . -t dev-general-testing-service && ^
docker run -d --name DevGeneralTestingService -p 49059:80 ^
--env-file ./../../secrets/secrets.dev.list ^
-e ASPNETCORE_ENVIRONMENT=DockerDev ^
-it dev-general-testing-service
echo finish dev-general-testing-service
docker image prune -f
pause
