install Docker steps: 
isnatll Docker from https://hub.docker.com/
install rabbitmq from https://hub.docker.com/_/rabbitmq
run the commnad set confuguration setting 

$ docker run -d  --name my-rabbit -p 15672:15672 -p 5672:5672  rabbitmq:management
