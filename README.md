install Docker steps: 
isnatll Docker from https://hub.docker.com/
install rabbitmq from https://hub.docker.com/_/rabbitmq
run the commnad set confuguration setting 

$ docker run -d  --name my-rabbit -p 15672:15672 -p 5672:5672  rabbitmq:management

----------------------
yml file settings

version: '3.8'

services:
  myapp:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "5000:80"  # Assuming your .NET Core app listens on port 80 inside the container
    depends_on:
      - rabbitmq

  rabbitmq:
    image: rabbitmq:3-management
    ports:
      - "5672:5672"  # RabbitMQ default port for AMQP
      - "15672:15672"  # RabbitMQ management console port
    environment:
      RABBITMQ_DEFAULT_USER: "guest"
      RABBITMQ_DEFAULT_PASS: "guest"

  postgres:
    image: postgres
    environment:
      POSTGRES_DB: "real_estate_db"
      POSTGRES_USER: "postgres"
      POSTGRES_PASSWORD: "admin"
    ports:
      - "5432:5432"

