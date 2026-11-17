# Software Engineering Lab 9 and Lab 10

This solution implements the testing and deployment exercises from the two supplied lab handouts.

## Included work

- `BasicMath`: arithmetic operations with equivalence-partition and boundary-value tests.
- `BankAccountApp`: debit, credit, and withdrawal rules with inline and CSV-driven tests.
- `LoginAutomation`: NUnit and Selenium tests driven by `LoginTestData.csv`.
- `LabWebApp`: ASP.NET Core MVC target application with a testable login page and health endpoint.
- Docker and Docker Compose configuration for Windows or Ubuntu hosts.
- GitHub Actions CI/CD, plus Azure and AWS deployment scripts.
- `database/lab_users.sql`, containing the reproducible login-user schema and demonstration rows requested for submission.

## Run locally

```powershell
dotnet restore Lab09_10.sln
dotnet test tests/LabUnitTests/LabUnitTests.csproj
dotnet run --project src/LabWebApp/LabWebApp.csproj --urls http://localhost:8080
```

Open `http://localhost:8080`. In another terminal, run the browser tests:

```powershell
$env:LAB_APP_URL = "http://localhost:8080"
dotnet test tests/LoginAutomation/LoginAutomation.csproj
```

## Run with Docker

```powershell
docker compose up --build -d
docker compose ps
```

Open `http://localhost:8080` and verify `http://localhost:8080/health`.

## Publish to Docker Hub

```powershell
docker login
docker tag se-lab-web:latest YOUR_DOCKERHUB_USERNAME/se-lab-web:latest
docker push YOUR_DOCKERHUB_USERNAME/se-lab-web:latest
```

Replace the placeholder username before running the commands. Cloud scripts also require the account-specific environment variables shown inside each file.

## CI/CD secrets

The GitHub Actions publish job expects repository secrets named `DOCKER_USERNAME` and `DOCKER_TOKEN`. Pull requests run restore, unit tests, the web build, and a Docker build without publishing an image.
