using Cardano.Rover.Core.Commands.External;

namespace Cardano.Rover.Core.CommandHandlers
{
	public class CreateGridCommandHandler
	{
		private readonly IMarsRoverGrid _marsRoverGrid;

		public CreateGridCommandHandler(IMarsRoverGrid marsRoverGrid)
		{
			_marsRoverGrid = marsRoverGrid;
		}
		
		public void Handle(GridAndRoverMovementsCommand command)
		{
			_marsRoverGrid.SetMarsRoverGridBoundary(command.GridSize.Y, command.GridSize.X);
		}
	}
}