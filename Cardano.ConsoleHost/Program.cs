using System;
using System.Linq;
using Cardano.Rover.Core;
using Cardano.Rover.Core.CommandHandlers;

namespace Cardano.ConsoleHost
{
	class Program
	{
		static void Main(string[] args)
		{
			//"5 5\r\n1 2 N\r\nLML\r\n3 4 E\r\nMM$"
			var stringCommand = "10 10\r\n1 2 N\r\nLML\r\n3 4 E\r\nMMR\r\n6 6 N\r\nMMMLMM";
			var commandParser = new CommandParser();
			var roverProvider = new RoverProvider();
			var roverDisplay = new DisplayRoverInformation(roverProvider);
			var roverGrid = new MarsRoverGrid();
			var commands = commandParser.CreateCommandsFrom(stringCommand);
			var createGridCommandHandler = new CreateGridCommandHandler(roverGrid);
			var createRoverCommandHandler = new CreateRoverCommandHandler(roverProvider);
			var roverActionCommandHandler = new RoverActionCommandHandler(roverGrid, new RoverActonToCoordinateTranslationService(new RoverActionToDirection()), roverProvider);
			var setRoverStartPositionCommandHandler = new SetRoverStartPositionCommandHandler(roverGrid);
			
			createGridCommandHandler.Handle(commands);
			createRoverCommandHandler.Handle(commands.RoverMovementCommands);
			setRoverStartPositionCommandHandler.Handle(commands.RoverMovementCommands);
			roverActionCommandHandler.Handle(commands);
			Console.WriteLine(roverDisplay.GetRoverInformation());
		}
	}
}