using System;
using Cardano.Rover.Core.Commands.Internal;
using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core.Entities
{
	public class Rover
	{
		public Guid RoverId { get; set; }
		public YAndXCoordinate GridPosition { get; set; }
		public Direction RoverFacingDirection { get; set; }
		
		public RoverStatus RoverStatus { get; set; }

		public Rover()
		{
			GridPosition = new YAndXCoordinate();
			RoverStatus = RoverStatus.Operational;
		}

		public void PerformRoverAction(SetRoverStateCommand roverStateCommand)
		{
			GridPosition.Y = roverStateCommand.YCoordinate;
			GridPosition.X = roverStateCommand.XCoordinate;
			RoverFacingDirection = roverStateCommand.FacingDirection;
		}
	}
}