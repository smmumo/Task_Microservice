
FROM ubuntu:24.04 AS builder

# install the .NET 8 SDK from the Ubuntu archive
# (no need to clean the apt cache as this is an unpublished stage)
RUN apt-get update && apt-get install -y dotnet8 ca-certificates
RUN apt-get install -y bash

# add your application code
WORKDIR /source
# using https://github.com/Azure-Samples/dotnetcore-docs-hello-world
#COPY . .
COPY ["Product.Api", "Product.Api/"]
COPY ["Product.Application", "Product.Application/"]
COPY ["Product.Infrastructure", "Product.Infrastructure/"]
COPY ["Product.Domain", "Product.Domain/"]

#RUN dotnet restore "./Product.Api/Product.Api.csproj"
# publish your ASP.NET app
#RUN dotnet publish -c Release -o /app --self-contained false
RUN dotnet publish Product.Api/Product.Api.csproj -c Release -o  /app --self-contained false

FROM ubuntu.azurecr.io/dotnet-aspnet:8.0-24.04_stable

WORKDIR /app
COPY --from=builder /app ./

ENV PORT 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Product.Api.dll"]
