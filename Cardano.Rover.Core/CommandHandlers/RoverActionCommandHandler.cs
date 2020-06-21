using System;
using System.Linq;
using System.Text;
using Cardano.Rover.Core.Commands;
using Cardano.Rover.Core.Enums;
using Cardano.Rover.Core.Exceptions;

namespace Cardano.Rover.Core
{
	public class RoverActionCommandHandler
	{
		private readonly IMarsRoverGrid _marsRoverGrid;
		private readonly IRoverActonToCoordinateTranslationService _roverActonToCoordinateTranslationService;
		private readonly IRoverProvider _roverProvider;

		public RoverActionCommandHandler(
			IMarsRoverGrid marsRoverGrid,
			IRoverActonToCoordinateTranslationService roverActonToCoordinateTranslationService,
			IRoverProvider roverProvider)
		{
			_marsRoverGrid = marsRoverGrid;
			_roverActonToCoordinateTranslationService = roverActonToCoordinateTranslationService;
			_roverProvider = roverProvider;
		}


		public void Handle(GridAndRoverMovementsCommand command)
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
	}
}