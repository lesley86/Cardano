using System;
using System.Collections.Generic;
using System.Linq;
using Cardano.Rover.Core.Commands.Internal;
using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public class RoverProvider : IRoverProvider
	{
		private List<Entities.Rover> _rovers;

		public RoverProvider()
		{
			_rovers = new List<Entities.Rover>();
		}

		public IEnumerable<Entities.Rover> GetAllRovers()
		{
			return _rovers;
		}

		public Guid CreateARover(YAndXCoordinate startingGridPosition, RoverStatus roverStatus,
			Direction roverStartDirection)
		{
			var result = new Entities.Rover
			{
				GridPosition = startingGridPosition,
				RoverStatus = roverStatus,
				RoverFacingDirection = roverStartDirection,
				RoverId = Guid.NewGuid()
			};
			
			_rovers.Add(result);
			return result.RoverId;
		}

		public void SaveRoverMovement(Guid roverId, SetRoverStateCommand roverStateCommand)
		{
			var rover = _rovers.FirstOrDefault(x => x.RoverId == roverId);
			rover.RoverFacingDirection = roverStateCommand.FacingDirection;
			rover.GridPosition.X = roverStateCommand.XCoordinate;
			rover.GridPosition.Y = roverStateCommand.YCoordinate;

		}
		
		public void UpdateRoverStatus(Guid roverId, RoverStatus roversStatus)
		{
			var rover = _rovers.FirstOrDefault(x => x.RoverId == roverId);
			rover.RoverStatus = roversStatus;
		}

		public Entities.Rover GetRoverStateFor(Guid roverId)
		{
			var result = _rovers.FirstOrDefault(x => x.RoverId == roverId);
			return result;
		}
	}
}