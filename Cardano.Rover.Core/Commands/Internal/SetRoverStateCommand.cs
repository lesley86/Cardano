using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core.Commands.Internal
{
	public class SetRoverStateCommand
	{
		public int YCoordinate { get; set; }
		public int XCoordinate { get; set; }
		public Direction FacingDirection { get; set; }
	}
}