using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public class RoverActionToDirection : IRoverActionToDirection
	{
		public Direction GetDirection(Direction startingDirection, RoverAction action)
		{
			var directionNumericalValue = (int) startingDirection;
			if (action == RoverAction.TurnRight)
			{
				directionNumericalValue++;
			}
			
			if (action == RoverAction.TurnLeft)
			{
				directionNumericalValue--;
			}

			if (directionNumericalValue > 4)
			{
				directionNumericalValue = 1;
			}

			if (directionNumericalValue < 1)
			{
				directionNumericalValue = 4;
			}

			return (Direction) directionNumericalValue;
		}
	}
}