using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core.Commands.External
{
	public class RoverActionCommand
	{
		public int ActionExecutionOrder { get; set; }
		public RoverAction RoverAction { get; set; }
	}
}