aws dynamodb create-table \
    --table-name Simulation \
    --attribute-definitions \
        AttributeName=PK,AttributeType=S \
        AttributeName=SK,AttributeType=S \
    --key-schema \
        AttributeName=PK,KeyType=HASH \
        AttributeName=SK,KeyType=RANGE \
    --provisioned-throughput ReadCapacityUnits=1,WriteCapacityUnits=1 \
    --endpoint-url http://localhost:8000/

aws dynamodb list-tables --endpoint-url http://localhost:8000/

aws dynamodb scan --table-name Simulation --endpoint-url http://localhost:8000/

aws dynamodb scan --table-name Simulation --select "COUNT" --endpoint-url http://localhost:8000/

aws dynamodb delete-table --table-name Simulation --endpoint-url http://localhost:8000/

https://docs.microsoft.com/pt-br/aspnet/core/web-api/handle-errors?view=aspnetcore-6.0