# Task_Microservice

Task_Microservice Using Clean Architecture AND CQRS

# how to run

docker compose up -d

# access service  using api gateway

# auth service
http://localhost:5200/auth/swagger/index.html
# to access order service
http://localhost:5200/orders/swagger/index.html
# to access product service 
http://localhost:5200/products/swagger/index.html


# or access via individual port 
# for auth service
http://localhost:5300/swagger/index.html
# for product service
http://localhost:5100/swagger/index.html
# for order
http://localhost:5168/swagger/index.html


Application: Contains CQRS command/query handlers and business logic.

Domain: Defines aggregates, entities, and encapsulates domain logic.

Infrastructure: Handles SQL persistence, and external dependencies

Domain-Driven Design

# Tech Stack

Layer Tool / Library
Framework .NET 8
Docker, Docker Compose

#docker compose up --build --force-recreate apigateway.services.api
#docker compose up --force-recreate apigateway.services.api