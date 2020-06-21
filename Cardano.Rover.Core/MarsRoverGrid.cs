using Cardano.Rover.Core.Enums;
using Cardano.Rover.Core.Exceptions;

namespace Cardano.Rover.Core
{
	public interface IMarsRoverGrid
	{
		int? GetFloorSpaceLength();
		int? GetGridWidth();

		void MoveRoverTo(Entities.Rover rover, int destinationYCoordinate, int destinationXCoordinate);
		
		void SetMarsRoverGridBoundary(int length, int breadth);
	}

	public class MarsRoverGrid : IMarsRoverGrid
	{
		private FloorSpaceStatus[,] _roverFloorSpace2dArray;

		public int? GetFloorSpaceLength()
		{
			return _roverFloorSpace2dArray?.GetLength(0);
		}

		public int? GetGridWidth()
		{
			return _roverFloorSpace2dArray?.GetLength(1);
		}

		public void MoveRoverTo(Entities.Rover rover, int destinationYCoordinate, int destinationXCoordinate)
		{
			ValidateReceivedCoordinatesIntegrity(destinationYCoordinate, destinationXCoordinate);
			ValidateCoordinatesAreWithInBoundary(destinationYCoordinate, destinationXCoordinate);
			if (rover.GridPosition.X != destinationXCoordinate || rover.GridPosition.Y != destinationYCoordinate)
			{
				ValidateIfDestinationIsOccupied(destinationYCoordinate, destinationXCoordinate);
			}
			
			_roverFloorSpace2dArray[rover.GridPosition.Y, rover.GridPosition.X] = FloorSpaceStatus.Vacant;
			_roverFloorSpace2dArray[destinationYCoordinate, destinationXCoordinate] = FloorSpaceStatus.Occupied;
		}
		
		public void SetMarsRoverGridBoundary(int length, int breadth)
		{
			_roverFloorSpace2dArray = new FloorSpaceStatus[length, breadth];
		}

		private void ValidateCoordinatesAreWithInBoundary(int yCoordinate, int xCoordinate)
		{
			if (yCoordinate > (_roverFloorSpace2dArray.GetLength(0) -1 ))
			{
				throw new RoverWillDriveOffGridException("Please place the mars rover within the floor Boundary");
			}

			if (xCoordinate > (_roverFloorSpace2dArray.GetLength(1) -1))
			{
				throw new RoverWillDriveOffGridException("Please place the mars rover within the floor Boundary");
			}
		}

		private void ValidateFloorSpaceExists()
		{
			if (_roverFloorSpace2dArray == null)
			{
				throw new FloorSpaceNotCreatedException("Please setup the floor space boundaries for the Rover");
			}
		}

		private void ValidateIfDestinationIsOccupied(int yCoordinate, int xCoordinate)
		{
			if (_roverFloorSpace2dArray[yCoordinate, xCoordinate] == FloorSpaceStatus.Occupied)
			{
				throw new FloorSpaceOccupiedException("Something else is already in this floor space");
			}
		}

		private void ValidateReceivedCoordinatesIntegrity(int yCoordinate, int xCoordinate)
		{
			if (yCoordinate < 0)
			{
				throw new InvalidCoordinateException("Coordinates for the floor space start at 0");
			}

			if (xCoordinate < 0)
			{
				throw new InvalidCoordinateException("Coordinates for the floor space start at 0");
			}
		}
	}
}