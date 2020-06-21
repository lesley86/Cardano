using System.Collections.Generic;

namespace Cardano.Rover.Core.Commands.External
{
	public class GridAndRoverMovementsCommand
	{
		public YAndXCoordinate GridSize { get; set; }
		
		public List<RoverStartPositionAndMovementCommands> RoverMovementCommands { get; set; }

		public GridAndRoverMovementsCommand()
		{
			GridSize = new YAndXCoordinate();
			RoverMovementCommands = new List<RoverStartPositionAndMovementCommands>();
		}
		
	}
}