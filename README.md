# SaveTheTurtle

As per the task https://s3-eu-west-1.amazonaws.com/lgc-public/TurtleChallenge.pdf

The implementation is done using the .netcore version 2.2

# Component
The architecture loosely coupled, following the SOA pattern.

1. Clients contain the end-user applications, in our case the console applications
2. Domain contains the Model, Repositories, and data processing projects
3. Foundation is used to move all of your shared code at once place, so that the developers could utilize it where needed.

# File Schema
I'm using JSON file for input and parsing it directly into my model class

# IExecutor
This is the only interface exposed to client which has a method Run

The game is divided into 3 sequences
1. Pre-processing 
	I'm retrieving the files. De-serializing it into model, initializing the board and turtle model. 
2. Processing
	In the processing step, the game is executing the instructions based on the moves like move, rotate. Based on the initial configurations 
	these moves varies
3. Post-processing
	In the post-processing, it is checked whether the turtle has succeed in finding the way or just lost
	
# Tool used:
1. Visual Studio 2017

# Packages:
1. Newtonsoft.JSON

# How to execute from command-line
	1. Navigate to the Code folder
	2. Restore the nuget packages using the command-line
		dotnet restore
	3. Clean the solution
		dotnet clean
	4. Build the solution
		dotnet build
	5. For Running the code, move to the bin of Turtle.Client.ConsoleApp\bin\Debug\netcoreapp2.2 and run this command
		cd Turtle.Client.ConsoleApp\bin\Debug\netcoreapp2.2
		dotnet Turtle.Client.ConsoleApp.dll
		
Note: I have added the TestCases project, but I haven't provide the test cases yet. It is for later implementation.