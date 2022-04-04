up:
	cd src && dotnet build && docker-compose up -d dynamodb && dotnet run cd ..

tdd:
	cd test && dotnet watch test --logger "console;verbosity=detailed" && cd ..

test-run:
	cd test && dotnet test && cd ..

ssl: 
	cd src && dotnet dev-certs https --trust && cd ..