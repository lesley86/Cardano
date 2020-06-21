using Cardano.Rover.Core.Commands;
using Cardano.Rover.Core.Commands.External;

namespace Cardano.Rover.Core
{
	public interface ICommandParser
	{
		GridAndRoverMovementsCommand CreateCommandsFrom(string roverCommand);
	}
}