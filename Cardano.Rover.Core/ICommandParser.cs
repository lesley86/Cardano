using Cardano.Rover.Core.Commands;

namespace Cardano.Rover.Core
{
	public interface ICommandParser
	{
		GridAndRoverMovementsCommand CreateCommandsFrom(string roverCommand);
	}
}