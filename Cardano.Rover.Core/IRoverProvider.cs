using System;
using System.Collections.Generic;
using Cardano.Rover.Core.Commands.Internal;
using Cardano.Rover.Core.Enums;

namespace Cardano.Rover.Core
{
	public interface IRoverProvider
	{
		Guid CreateARover(YAndXCoordinate startingGridPosition, RoverStatus roverStatus,
			Direction roverStartDirection);

		void SaveRoverMovement(Guid roverId, SetRoverStateCommand roverStateCommand);

		Entities.Rover GetRoverStateFor(Guid roverId);
		
		IEnumerable<Entities.Rover> GetAllRovers();

		void UpdateRoverStatus(Guid roverId, RoverStatus roversStatus);
	}
}