using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public class RoverStartPositionCommand
	{
		public YAndXCoordinate GridPosition { get; set; }
		public Direction RoverFacingDirection { get; set; }
	}
}