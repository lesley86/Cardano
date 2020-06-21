using Cardano.Rover.Core.Commands.Internal;
using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public class RoverActonToCoordinateTranslationService : IRoverActonToCoordinateTranslationService
	{
		private readonly IRoverActionToDirection _roverActionToDirection;

		public RoverActonToCoordinateTranslationService(IRoverActionToDirection roverActionToDirection)
		{
			_roverActionToDirection = roverActionToDirection;
		}
		
		public SetRoverStateCommand Translate(Entities.Rover rover, RoverActionCommand roverActionCommand)
		{
			var result = new SetRoverStateCommand
			{
				XCoordinate = rover.GridPosition.X,
				YCoordinate = rover.GridPosition.Y
			};
			
			switch (roverActionCommand.RoverAction)
			{
				case RoverAction.TurnLeft:
					result.FacingDirection =
						_roverActionToDirection.GetDirection(rover.RoverFacingDirection, roverActionCommand.RoverAction);
					break;

				case RoverAction.TurnRight:
					result.FacingDirection =
						_roverActionToDirection.GetDirection(rover.RoverFacingDirection, roverActionCommand.RoverAction);
					break;

				case RoverAction.MoveForwardInTheDirectionRoverIsFacing:
					switch (rover.RoverFacingDirection)
					{
						case Direction.North:
							result.YCoordinate--;
							break;
						case Direction.South:
							result.YCoordinate++;
							break;
						case Direction.East:
							result.XCoordinate++;
							break;
						case Direction.West:
							result.XCoordinate--;
							break;
					}

					result.FacingDirection = rover.RoverFacingDirection;
					break;
			}

			return result;
		}
	}
}