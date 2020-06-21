using System.Collections.Generic;
using System.Linq;
using Cardano.Rover.Core.Commands.External;

namespace Cardano.Rover.Core.CommandHandlers
{
	public class SetRoverStartPositionCommandHandler
	{
		private readonly IMarsRoverGrid _marsRoverGrid;

		public SetRoverStartPositionCommandHandler(
			IMarsRoverGrid marsRoverGrid)
		{
			_marsRoverGrid = marsRoverGrid;
		}
		
		public void Handle(IEnumerable<RoverStartPositionAndMovementCommands> commands)
		{
			foreach (var roverStartPositionCommand in commands.OrderBy(x => x.RoverCreationOrder))
			{
				_marsRoverGrid.SetRoverStartPosition(roverStartPositionCommand.RoverStartPositionCommand.GridPosition.Y, roverStartPositionCommand.RoverStartPositionCommand.GridPosition.X);
			}
		}
	}
}