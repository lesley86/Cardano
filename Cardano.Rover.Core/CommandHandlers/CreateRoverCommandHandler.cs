using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public class CreateRoverCommandHandler
	{
		private readonly RoverProvider _roverProvider;

		public CreateRoverCommandHandler(RoverProvider roverProvider)
		{
			_roverProvider = roverProvider;
		}
		
		public void Handle(IEnumerable<RoverStartPositionAndMovementCommands> commands)
		{
			foreach (var roverCommands in commands.OrderBy(x => x.RoverCreationOrder))
			{
				var gridPosition = new YAndXCoordinate
				{
					X = roverCommands.RoverStartPositionCommand.GridPosition.X,
					Y = roverCommands.RoverStartPositionCommand.GridPosition.Y
				};
				var varRoverId = _roverProvider.CreateARover(gridPosition, RoverStatus.Operational,
					roverCommands.RoverStartPositionCommand.RoverFacingDirection);
				
				roverCommands.RoverId = varRoverId;
			}
		}
	}
}