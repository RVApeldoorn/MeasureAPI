# Measurement API

## Overview
This Measurement API is a .NET 8.0 backend service that manages patient sessions and measurement submisisons. Healthcare providers can assign sessions to patients, where each session can include one or multiple measurement requests (e.g., weight, height, blood pressure). Patients can fetch their sessions and submit measurement values via the companion Flutter app. The API uses JWT-based authentication, with setup codes validated against a database to issue tokens for secure communication. The API is built with ASP.NET Core and Entity Framework Core (SQLite).

## Setup Instructions

### Prerequisites
- **.NET 8.0 SDK**: Install from [Microsoft's official site](https://dotnet.microsoft.com/download/dotnet/8.0).
- **SQLite**: Ensure SQLite is installed or use an in-memory database for development.
- **REST Client**: Use tools like VS Code REST Client or Postman to test API endpoints via the `.rest` file.

### Installation
1. **Clone the Repository**:
   ```bash
   git clone git@github.com:RVApeldoorn/MeasureAPI.git
   cd MeasureAPI
   ```

2. **Restore Dependencies**:
   Run the following command to restore NuGet packages, including `Microsoft.AspNetCore.Authentication.JwtBearer` and `Microsoft.EntityFrameworkCore.Sqlite`:
   ```bash
   dotnet restore
   ```

3. **Configure the Database**:
   - The API automatically creates the SQLite database (`measurements.db`) and applies existing migrations and seeders on startup.  Seeders populate initial data (e.g., test patients, setup codes); see `DbSeeder.cs` for details.
   - Ensure the connection string in `appsettings.json` is correct:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Data Source=measurements.db"
       }
     }
     ```
   - To use a different database location, update the `DefaultConnection` string accordingly.
   - Applying new migrations: if new migrations are added, apply them to your local database: 
     ```bash
     dotnet ef database update
     ```

4. **Environment Variables**:
   - Configure JWT settings in `appsettings.json` (or environment variables). Example:
     ```json
     {
       "Jwt": {
         "Key": "$7fP9@qW2z#X8lM0vB3rT5nK1pV6eA9dU2sC4mZ!",
         "Issuer": "MeasureApi",
         "Audience": "MeasureApp",
         "ExpiresInMinutes": "60"
       }
     }
     ```
   - Ensure the `Key` is a strong, secure value and not exposed in version control.

5. **Run the API**:
   Start the API in development mode:
   ```bash
   dotnet run
   ```
   The API will be available at `http://localhost:5005`.

### Patient setup process for API
- **Setup Code Validation**: When a user enters a setup code in the Flutter app, the API validates it against the database. If valid, it issues a JWT token, which is returned to the app for authentication in subsequent requests.
- **Session Management**: Healthcare providers can create sessions and assign measurement requests via API endpoints. Patients can fetch their sessions using the JWT token.
- **Measurement Submission**: Patients submit measurement values (e.g., weight, blood pressure) to open measurement requests in a session, identified by their JWT token.

### Testing and API Workings
- Refer to the `.rest` file in the repository for examples of all available API endpoints, including setup code validation, fetching patients, session creation, session fetching, and measurement submission.
- Use a REST client like VS Code REST Client or Postman to execute the requests in the `.rest` file and test the API.
- The API uses `Microsoft.AspNetCore.Authentication.JwtBearer` for token-based authentication and `Microsoft.EntityFrameworkCore.Sqlite` for data persistence.
- Unit tests are implemented using `xunit`, `Moq`, and `FluentAssertions`. Run tests with:
  ```bash
  dotnet test
  ```

## Technology Stack
The Measurement API uses the following technologies:

### Backend
- **.NET 8.0**: Core runtime and SDK for the API.
- **ASP.NET Core**: Framework for building the RESTful API, handling requests and middleware.
- **Entity Framework Core (9.0.4)**: ORM for database operations with SQLite (`measurements.db`).
- **SQLite**: Lightweight database for storing patient data and sessions.
- **JWT Authentication (Microsoft.AspNetCore.Authentication.JwtBearer 8.0.2)**: Secures endpoints with token-based authentication, configured in `appsettings.json`.

### Testing and Development Tools
- **xUnit (2.9.3), Moq (4.20.72), FluentAssertions (8.2.0)**: Unit testing frameworks and libraries, run with `dotnet test`.
- **Microsoft.EntityFrameworkCore.Tools (9.0.4)**: CLI tools for database migrations (`dotnet ef`).
- **REST Clients**: Postman or VS Code REST Client to test endpoints via `.rest` file.