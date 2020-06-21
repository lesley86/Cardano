namespace Cardano.Rover.Core
{
	public interface IMarsRoverGrid
	{
		int? GetFloorSpaceLength();
		int? GetGridWidth();
		void MoveRoverTo(Entities.Rover rover, int destinationYCoordinate, int destinationXCoordinate);
		void SetMarsRoverGridBoundary(int length, int breadth);
		void SetRoverStartPosition(int yCoordinate, int xCoordinate);
	}
}