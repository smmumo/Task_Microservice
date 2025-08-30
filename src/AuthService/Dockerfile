FROM ubuntu:24.04 AS builder

# install the .NET 8 SDK from the Ubuntu archive
# (no need to clean the apt cache as this is an unpublished stage)
RUN apt-get update && apt-get install -y dotnet8 ca-certificates
RUN apt-get install -y bash
# add your application code
WORKDIR /source

COPY . .

RUN ls -la
# publish your ASP.NET app
RUN dotnet publish AuthService.csproj -c Release -o /app --self-contained false

FROM ubuntu/dotnet-aspnet:8.0-24.04_stable
WORKDIR /app
COPY --from=builder /app ./

ENV ASPNETCORE_ENVIRONMENT=Development
ENV PORT 8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "/app/AuthService.dll"]