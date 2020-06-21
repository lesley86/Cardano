using System;
using System.Collections.Generic;
using System.Linq;
using Cardano.Rover.Core.Commands;
using Cardano.Rover.Core.Enums;
using Cardano.Rover.Core.Exceptions;

namespace Cardano.Rover.Core
{
	public class CommandParser : ICommandParser
	{
		public GridAndRoverMovementsCommand CreateCommandsFrom(string roverCommand)
		{
			var result = new GridAndRoverMovementsCommand();
			var movementCommandsIncludingHeaderArray = ValidateCommandStructure(roverCommand);
			var gridSizeArray = movementCommandsIncludingHeaderArray[0].Split(' ');
			CreateRoverGridSizeCommand(gridSizeArray, result);
			CreateRoverStartPositionAndMovementCommands(movementCommandsIncludingHeaderArray, result);
			return result;
		}

		private static void CreateRoverStartPositionAndMovementCommands(string[] movementCommandsIncludingHeaderArray,
			GridAndRoverMovementsCommand result)
		{
			var roverCreationOrder = 1;
			for (var i = 1; i < movementCommandsIncludingHeaderArray.Length; i += 2)
			{
				CreateRoverStartPositionAndMovementCommand(movementCommandsIncludingHeaderArray, i, result, roverCreationOrder);
				roverCreationOrder++;
			}
		}

		private static void CreateRoverGridSizeCommand(string[] gridSizeArray, GridAndRoverMovementsCommand result)
		{
			var coordinatesHolder = 0;
			if (!int.TryParse(gridSizeArray[0], out coordinatesHolder))
			{
				throw new InvalidCommandStructureException(
					"The xCoordinate of the grid structure is not a valid integer.");
			}

			result.GridSize.Y = coordinatesHolder;

			if (!int.TryParse(gridSizeArray[1], out coordinatesHolder))
			{
				throw new InvalidCommandStructureException(
					"The xCoordinate of the grid structure is not a valid integer.");
			}

			result.GridSize.X = coordinatesHolder;
		}

		private static void CreateRoverStartPositionAndMovementCommand(string[] movementCommandsIncludingHeaderArray,
			int i,
			GridAndRoverMovementsCommand result,
			int roverCreationOrder)
		{
			var roverStartPositionCommand = RoverStartPositionCommand(movementCommandsIncludingHeaderArray, i);
			var roverActionCommands = MovementCharsCommand(movementCommandsIncludingHeaderArray, i);
			result.RoverMovementCommands.Add(
				new RoverStartPositionAndMovementCommands
				{
					RoverId = Guid.NewGuid(),
					RoverStartPositionCommand = roverStartPositionCommand,
					RoverMovementCommands = roverActionCommands,
					RoverCreationOrder = roverCreationOrder
				});
		}

		private static IEnumerable<RoverActionCommand> MovementCharsCommand(string[] movementCommandsIncludingHeaderArray, int i)
		{
			var result = new List<RoverActionCommand>();
			var actionExecutionOrder = 1;
			var movementsStringArray = movementCommandsIncludingHeaderArray[i + 1];
			var movementChars = movementsStringArray.Select(x => x);
			foreach (var movementChar in movementChars)
			{
				switch (movementChar)
				{
					case 'L':
						CreatRoverActionCommand(result, RoverAction.TurnLeft, actionExecutionOrder);
						actionExecutionOrder++;
						break;
					case 'R':
						CreatRoverActionCommand(result, RoverAction.TurnRight, actionExecutionOrder);
						actionExecutionOrder++;
						break;
					case 'M':
						CreatRoverActionCommand(result, RoverAction.MoveForwardInTheDirectionRoverIsFacing, actionExecutionOrder);
						actionExecutionOrder++;
						break;
				}
			}
			
			return result;
		}

		private static void CreatRoverActionCommand(List<RoverActionCommand> result, RoverAction roverAction, int actionExecutionOrder)
		{
			result.Add(new RoverActionCommand
			{
				RoverAction = roverAction,
				ActionExecutionOrder = actionExecutionOrder
			});
		}

		private static RoverStartPositionCommand RoverStartPositionCommand(
			string[] movementCommandsIncludingHeaderArray, int i)
		{
			var startPositionStringArray = movementCommandsIncludingHeaderArray[i].Split(' ');
			var roverStartPositionCommand = SetStartPositionCoordinates(startPositionStringArray);
			SetStartPositionDirection(startPositionStringArray, roverStartPositionCommand);
			return roverStartPositionCommand;
		}

		private static void SetStartPositionDirection(string[] startPositionStringArray,
			RoverStartPositionCommand roverStartPositionCommand)
		{
			switch (char.Parse(startPositionStringArray[2]))
			{
				case 'N':
					roverStartPositionCommand.RoverFacingDirection = Direction.North;
					break;
				case 'E':
					roverStartPositionCommand.RoverFacingDirection = Direction.East;
					break;
				case 'W':
					roverStartPositionCommand.RoverFacingDirection = Direction.West;
					break;
				case 'S':
					roverStartPositionCommand.RoverFacingDirection = Direction.South;
					break;
			}
		}

		private static RoverStartPositionCommand SetStartPositionCoordinates(string[] startPositionStringArray)
		{
			var gridXAndYLength = new YAndXCoordinate();
			var roverStartPositionCommand = new RoverStartPositionCommand();
			gridXAndYLength.Y = int.Parse(startPositionStringArray[0]);
			gridXAndYLength.X = int.Parse(startPositionStringArray[1]);
			roverStartPositionCommand.GridPosition = gridXAndYLength;
			return roverStartPositionCommand;
		}

		private static string[] ValidateCommandStructure(string roverCommand)
		{
			var movementCommandsIncludingHeaderArray =
				roverCommand.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
			if (movementCommandsIncludingHeaderArray.Length < 3)
			{
				throw new InvalidCommandStructureException(
					"A command for the rover must at least include a grid size, starting position and movement pattern");
			}

			if (((movementCommandsIncludingHeaderArray.Length - 1) % 2) != 0)
			{
				throw new InvalidCommandStructureException(
					"The movement command must be 1 grid size row followed by 1 or more pairs of data");
			}

			return movementCommandsIncludingHeaderArray;
		}
	}
}