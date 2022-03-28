up:
	dotnet build && docker-compose up

tdd:
	cd test && dotnet watch test --logger "console;verbosity=detailed" && cd ..