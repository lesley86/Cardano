using System;
using System.Linq;
using System.Text;
using Cardano.Rover.Core.Commands;
using Cardano.Rover.Core.Enums;
using Cardano.Rover.Core.Exceptions;

namespace Cardano.Rover.Core
{
	public class RoverService
	{
		private readonly ICommandParser _commandParser;
		private readonly IMarsRoverGrid _marsRoverGrid;
		private readonly IRoverActonToCoordinateTranslationService _roverActonToCoordinateTranslationService;
		private readonly IRoverProvider _roverProvider;

		public RoverService(
			ICommandParser commandParser,
			IMarsRoverGrid marsRoverGrid,
			IRoverActonToCoordinateTranslationService roverActonToCoordinateTranslationService,
			IRoverProvider roverProvider)
		{
			_commandParser = commandParser;
			_marsRoverGrid = marsRoverGrid;
			_roverActonToCoordinateTranslationService = roverActonToCoordinateTranslationService;
			_roverProvider = roverProvider;
		}


		public void InteractWithRover(string roverCommand)
		{
			var command = _commandParser.CreateCommandsFrom(roverCommand);
			_marsRoverGrid.SetMarsRoverGridBoundary(command.GridSize.Y, command.GridSize.X);
			CreateRover(command);
			ExecuteRoverActions(command);
		}
		
		public string GetRoverInformation()
		{
			var rovers = _roverProvider.GetAllRovers();
			var sb = new StringBuilder();
			foreach (var rover in rovers)
			{
				sb.Append($"" +
						$"Rover's y coordinate: {rover.GridPosition.Y}, " +
						$"Rover's x coordinate: {rover.GridPosition.X}, " +
						$"Rover's direction: {rover.RoverFacingDirection}, " +
						$"Rover's status: {rover.RoverStatus.ToString()}" +
						$"{Environment.NewLine}");
			}

			return sb.ToString();
		}

		private void ExecuteRoverActions(GridAndRoverMovementsCommand command)
		{
			foreach (var roverCommands in command.RoverMovementCommands.OrderBy(x => x.RoverCreationOrder))
			{
				foreach (var roverMovementCommand in roverCommands.RoverMovementCommands.OrderBy(
					x => x.ActionExecutionOrder))
				{
					var rover = _roverProvider.GetRoverStateFor(roverCommands.RoverId);
					var roverStateChangeCommand =
						_roverActonToCoordinateTranslationService.Translate(rover, roverMovementCommand);
					try
					{
						_marsRoverGrid.MoveRoverTo(rover, roverStateChangeCommand.YCoordinate,
							roverStateChangeCommand.XCoordinate);
						_roverProvider.SaveRoverMovement(rover.RoverId, roverStateChangeCommand);
					}
					catch (RoverWillDriveOffGridException e)
					{
						_roverProvider.UpdateRoverStatus(roverCommands.RoverId, RoverStatus.HelpRequired);
					}
					catch (FloorSpaceOccupiedException e)
					{
						_roverProvider.UpdateRoverStatus(roverCommands.RoverId, RoverStatus.HelpRequired);
					}
				}
			}
		}

		private void CreateRover(GridAndRoverMovementsCommand command)
		{
			foreach (var roverCommands in command.RoverMovementCommands.OrderBy(x => x.RoverCreationOrder))
			{
				var gridPosition = new YAndXCoordinate
				{
					X = roverCommands.RoverStartPositionCommand.GridPosition.X,
					Y = roverCommands.RoverStartPositionCommand.GridPosition.Y
				};
				var varRoverId = _roverProvider.CreateARover(gridPosition, RoverStatus.Operational,
					roverCommands.RoverStartPositionCommand.RoverFacingDirection);
				roverCommands.RoverId = varRoverId;

			}
		}
	}
}