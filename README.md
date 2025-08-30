# Task_Microservice

Task_Microservice Using Clean Architecture AND CQRS

# how to run

docker compose up -d

# access individual service 

# auth service
create user to test authorization
http://apigatewayserviceapi:5200/auth/swagger/index.html

# to access order service
http://apigatewayserviceapi:5200/orders/swagger/index.html

# to access product service 
http://apigatewayserviceapi:5200/products/swagger/index.html


Application: Contains CQRS command/query handlers and business logic.

Domain: Defines aggregates, entities, and encapsulates domain logic.

Infrastructure: Handles SQL persistence, and external dependencies

Domain-Driven Design

# Tech Stack

Layer Tool / Library
Framework .NET 8
Docker, Docker Compose

#docker compose up --build --force-recreate
#docker compose up --force-recreate apigateway.services.api