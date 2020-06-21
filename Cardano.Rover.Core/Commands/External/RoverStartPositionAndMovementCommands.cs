using System;
using System.Collections.Generic;

namespace Cardano.Rover.Core.Commands.External
{
	public class RoverStartPositionAndMovementCommands
	{
		public Guid RoverId { get; set; }
		public RoverStartPositionCommand RoverStartPositionCommand { get; set; }
		public IEnumerable<RoverActionCommand> RoverMovementCommands { get; set; }
		
		public int RoverCreationOrder { get; set; }

		public RoverStartPositionAndMovementCommands()
		{
			RoverMovementCommands = new List<RoverActionCommand>();
			RoverStartPositionCommand = new RoverStartPositionCommand();
		}

	}
}