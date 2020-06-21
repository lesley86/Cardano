using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public class RoverActionCommand
	{
		public int ActionExecutionOrder { get; set; }
		public RoverAction RoverAction { get; set; }
	}
}