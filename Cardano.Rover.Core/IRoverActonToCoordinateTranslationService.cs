using Cardano.Rover.Core.Commands.Internal;

namespace Cardano.Rover.Core
{
	public interface IRoverActonToCoordinateTranslationService
	{
		SetRoverStateCommand Translate(Entities.Rover rover, RoverActionCommand roverActionCommand);
	}
}