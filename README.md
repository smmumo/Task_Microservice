# Task_Microservice

Task_Microservice Using Clean Architecture AND CQRS

# how to run

docker compose up -d

# to access order service
http://localhost:5000/swagger/index.html

# to access product service 
http://localhost:6000/swagger/index.html


Application: Contains CQRS command/query handlers and business logic.

Domain: Defines aggregates, entities, and encapsulates domain logic.

Infrastructure: Handles SQL persistence, and external dependencies

Domain-Driven Design

# Tech Stack

Layer Tool / Library
Framework .NET 8
Docker, Docker Compose