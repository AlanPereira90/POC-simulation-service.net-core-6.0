# Simulation Service

### What is?
Simulation-service is a POC used to study DDD and TDD with ASP.NET core 6.0

### How to use in development environment?
Run the command `make up` to up local dynamoDB (port 8000) and simulation-service (ports 5050 and 5051) containers.

### How to test?
Run the command `make tdd` to run tests and keep watching the code changes, and `make test-run` to execute a single test execution.

#### Important: 
After up the local dynamoDB instance at the first time, it's necessary create the single table *Simulations* to execute local end to end tests.

#### Dynamo DB utils commands:
- To create the table Simulations:
```
aws dynamodb create-table \

--table-name Simulations \

--attribute-definitions \

AttributeName=PK,AttributeType=S \

AttributeName=SK,AttributeType=S \

--key-schema \

AttributeName=PK,KeyType=HASH \

AttributeName=SK,KeyType=RANGE \

--provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 \

--endpoint-url http://localhost:8000/
```
- To list Dynamo DB created tables:
```
aws dynamodb list-tables --endpoint-url http://localhost:8000/
```
- To scan Dynamo DB Simulations Table:
```
aws dynamodb scan --table-name Simulations --endpoint-url http://localhost:8000/
```
- To remove Dynamo DB Simulations Table:
```
aws dynamodb delete-table --table-name Simulations --endpoint-url http://localhost:8000/
```

