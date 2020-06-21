using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public interface IRoverActionToDirection
	{
		Direction GetDirection(Direction startingDirection, RoverAction action);
	}
}