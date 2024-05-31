# Real Estate Doc Flow

## Description
Set up Docker containers for a .NET Core application, RabbitMQ, and PostgreSQL using Docker Compose.

## Prerequisites
- Docker installed on your system. You can download and install Docker from [Docker Hub](https://hub.docker.com/).

## Installation

### 1. Install Docker
- Visit [Docker Hub](https://hub.docker.com/) and follow the instructions to download and install Docker for your operating system.

### 2. Install RabbitMQ
- Visit [RabbitMQ Docker Hub page](https://hub.docker.com/_/rabbitmq) and choose the version you want to use.
- Run the following command in your terminal or command prompt to pull and run the RabbitMQ container:
  ```bash
  docker run -d --name my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:management
