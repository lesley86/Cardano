using Cardano.Rover.Core.Commands;

namespace Cardano.Rover.Core
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