# Whalebone MicroserviceApp

A simple .NET 8 microservice for storing and retrieving person data via REST API, using SQL Server and Entity Framework Core.

---

## Features

- **POST /person/save**: Store a person record in the database.
- **GET /person/{id}**: Retrieve a person record by external ID.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) (for local build/test)

---

## Build & Run with Docker Compose

1. **Start the service and database:**
```
docker compose up --build
```

- The API will be available at [http://localhost:8080](http://localhost:8080).
   - SQL Server will be available on port 1433 (internal to Docker).

2. **Stop the services:**
```
docker compose down
```

---

## API Usage

### Store a Person
```
curl -X POST http://localhost:8080/person/save 
-H "Content-Type: application/json" 
-d '{ "externalId": "11111111-1111-1111-1111-111111111111", "name": "John Doe", "email": "john@example.com", "dateOfBirth": "2000-01-01T00:00:00" }'
```

### Retrieve a Person
```
curl http://localhost:8080/person/11111111-1111-1111-1111-111111111111
```

---

## Running Integration Tests

  1. From the solution root, run:
```
dotnet test Whalebone.MicroserviceApp.IntegrationTests
```

- **Against Dockerized API:**

  1. Ensure the containers are running.

---

## Notes

- **Credentials:**  
  The database credentials in `appsettings.json` and `docker-compose.yml` are for development/demo only.  
  **In production, use secure secret management (e.g., environment variables, Azure Key Vault, AWS Secrets Manager, or Docker secrets).**
