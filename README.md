# PacSim
This was an exercise I used to try out some new C# 8 features (ex. nullable, switch statements, immutable structs). 

The structure is indicative of how I generally setup an app's data layer and unit tests, but I stopped short of making the service calls awaitable and using dependency injection. 

There is a console app that takes input one command per line and a full set of unit tests that show the use of Moq for service mocking.

# Original Requirements
Source: https://github.com/ie/Code-Challenge-1 (23 Nov 2019)

Pacman Simulator

Description:

The application is a simulation of Pacman moving on in a grid, of dimensions 5 units x 5 units.
There are no other obstructions on the grid.
Pacman is free to roam around the surface of the grid, but must be prevented from moving off the grid. Any movement that would result in Pacman moving off the grid must be prevented, however further valid movement commands must still be allowed.
Create an application that can read in commands of the following form -
PLACE X,Y,F

MOVE

LEFT

RIGHT

REPORT

PLACE will put the Pacman on the grid in positon X,Y and facing NORTH,SOUTH, EAST or WEST.

The origin (0,0) can be considered the SOUTH WEST most corner.

The first valid command to Pacman is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command. The application should discard all commands in the sequence until a valid PLACE command has been executed.

MOVE will move Pacman one unit forward in the direction it is currently facing.

LEFT and RIGHT will rotate Pacman 90 degrees in the specified direction without changing the position of Pacman.

REPORT will announce the X,Y and F of Pacman. This can be in any form, but standard output is sufficient.

Pacman that is not on the grid can choose the ignore the MOVE, LEFT, RIGHT and REPORT commands.

Input can be from a file, or from standard input, as the developer chooses.

Provide test data to exercise the application.

Constraints:

Pacman must not move off the grid during movement. This also includes the initial placement of Pacman.
Any move that would cause Pacman to fall must be ignored.
