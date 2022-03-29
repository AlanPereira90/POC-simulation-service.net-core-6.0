up:
	dotnet build && docker-compose up

tdd:
	cd test && dotnet watch test --logger "console;verbosity=detailed" && cd ..

ssl: 
	cd src && dotnet dev-certs https --trust && cd ..