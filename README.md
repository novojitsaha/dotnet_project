# GameStore API

A small ASP.NET Core backend project for managing game catalog data. This project was built as a portfolio piece to demonstrate practical backend engineering skills in C#, including API design, DTO-based contracts, Entity Framework Core, relational modeling, validation, and database lifecycle management.

This project exposes a minimal API for working with games, persists data with SQLite, and applies schema changes automatically through EF Core migrations at startup.


## Tech Stack

- C#
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQLite
- Data annotations for request validation
- EF Core migrations and seed data initialization

## What the API Does

The API currently supports the following game catalog operations:

- Retrieve all games
- Retrieve a single game by id
- Create a new game

The `/games` resource is the current focus of the application. The codebase also contains scaffolding for update and delete operations, which makes the next iteration straightforward.

## Architecture Overview

The project is organized into a few focused layers:

- `Endpoints`
  Defines HTTP routes and request handling using ASP.NET Core Minimal APIs.
- `Dtos`
  Contains request and response contracts so the API surface is decoupled from persistence entities.
- `Models`
  Defines the domain entities mapped to the database.
- `Data`
  Contains the EF Core `DbContext`, database registration, migrations, and startup migration/seed behavior.

This structure keeps responsibilities clear without introducing unnecessary abstraction for a project of this size.


## Data Model

- `Game`
  Represents a game with a name, genre, price, and release date.
- `Genre`
  Represents a reference category used by games.



## Validation

Input validation is handled at the DTO layer with data annotations. Current validation covers constraints such as:

- required name values
- string length limits
- valid numeric ranges for identifiers and price

This shows API-boundary validation rather than pushing all correctness checks into database errors.

## API Endpoints

Base URL during local development:

```text
http://localhost:5242
```

Current endpoints:

```http
GET    /games
GET    /games/{id}
POST   /games
```

Example create request:

```http
POST /games
Content-Type: application/json

{
  "name": "Fifa 26",
  "genreId": 3,
  "price": 49.99,
  "releaseDate": "2026-10-20"
}
```

## Running the Project

### Prerequisites

- .NET SDK with support for `net10.0`

### Start the API

From the repository root:

```powershell
dotnet run --project .\GameStore.Api\
```

Once the application starts, it will:

- build the web host
- register the SQLite-backed EF Core context
- apply database migrations
- seed default genres if they do not already exist

## Local Development Notes

- The SQLite database is stored locally as `GameStore.db`
- Connection settings live in `GameStore.Api/appsettings.json`
- HTTP request samples are included in `GameStore.Api/games.http`
- Launch settings expose the app locally on `http://localhost:5242` and `https://localhost:7193`

## What This Project Demonstrates

## Roadmap

Logical next improvements would be:

- finish update and delete endpoints
- add integration tests for endpoint and database behavior
- add structured error responses
- introduce pagination and filtering for the game catalog
