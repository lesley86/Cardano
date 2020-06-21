using System.Linq;
using Cardano.Rover.Core;
using Cardano.Rover.Core.Enums;
using NUnit.Framework;

namespace Cardano.Core.UnitTests
{
	public class CommandParserUnitTest
	{
		private CommandParser _commandParser;
		
		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void ShouldParseCommandStringIntoCommandStructure()
		{
			_commandParser = new CommandParser();
			var result = _commandParser.CreateCommandsFrom($"5 6\r\n1 2 N\r\nLML\r\n3 4 E\r\nMM$");

			var firstRoverCommand = result.RoverMovementCommands.OrderBy(rover => rover.RoverCreationOrder).First();
			var secondRoverCommand =
				result.RoverMovementCommands.OrderBy(rover => rover.RoverCreationOrder).Skip(1).First();
			Assert.AreEqual(5, result.GridSize.Y);;
			Assert.AreEqual(6, result.GridSize.X);
			Assert.AreEqual(1, firstRoverCommand.RoverStartPositionCommand.GridPosition.Y);
			Assert.AreEqual(2, firstRoverCommand.RoverStartPositionCommand.GridPosition.X);
			Assert.AreEqual(Direction.North, firstRoverCommand.RoverStartPositionCommand.RoverFacingDirection);
			Assert.AreEqual(3, firstRoverCommand.RoverMovementCommands.Count());
			Assert.AreEqual(3, secondRoverCommand.RoverStartPositionCommand.GridPosition.Y);
			Assert.AreEqual(4, secondRoverCommand.RoverStartPositionCommand.GridPosition.X);
			Assert.AreEqual(Direction.East, secondRoverCommand.RoverStartPositionCommand.RoverFacingDirection);
			Assert.AreEqual(2, secondRoverCommand.RoverMovementCommands.Count());
		}
		
	}
}