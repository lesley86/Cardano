using System;
using Cardano.Rover.Core;

namespace Cardano.ConsoleHost
{
	class Program
	{
		static void Main(string[] args)
		{
			var RoverService = new RoverService(new CommandParser(), new MarsRoverGrid(), new RoverActonToCoordinateTranslationService(new RoverActionToDirection()), new RoverProvider());
			RoverService.InteractWithRover("5 5\r\n1 2 N\r\nLML\r\n3 4 E\r\nMM$");
			Console.WriteLine(RoverService.GetRoverInformation());
		}
	}
}