using Cardano.Rover.Core;
using Cardano.Rover.Core.Exceptions;
using NUnit.Framework;

namespace Cardano.Core.UnitTests
{
	[TestFixture]
	public class MarsFloorSpaceArrayTests
	{
		private MarsRoverGrid _marsRoverGrid;
		
		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void ShouldCreateGridOfSpecifiedSize()
		{
			_marsRoverGrid = new MarsRoverGrid();
			_marsRoverGrid.SetMarsRoverGridBoundary(5,6);
			Assert.AreEqual(5,_marsRoverGrid.GetFloorSpaceLength());
			Assert.AreEqual(6,_marsRoverGrid.GetGridWidth());
		}
		
		[Test]
		public void ShouldThrowExceptionSetPositionIsCalledAndGridIsAlreadyOccupied()
		{
			_marsRoverGrid = new MarsRoverGrid();
			_marsRoverGrid.SetMarsRoverGridBoundary(5,5);
			_marsRoverGrid.SetRoverStartPosition(3, 3);
			Assert.Throws<GridSpaceOccupiedException>(() => _marsRoverGrid.SetRoverStartPosition(3, 3));
		}
		
		[Test]
		public void ShouldThrowExceptionWhenYouMoveRoverOffEdge()
		{
			_marsRoverGrid = new MarsRoverGrid();
			_marsRoverGrid.SetMarsRoverGridBoundary(5,5);
			var rover = new Rover.Core.Entities.Rover() {GridPosition = new YAndXCoordinate {X = 2, Y = 2}};

			Assert.Throws<RoverWillDriveOffGridException>(() => _marsRoverGrid.MoveRoverTo(rover, 6, 5));
		}
		
		[Test]
		public void ShouldThrowExceptionWhenYouMoveRoverToACellThatIsOccupied()
		{
			_marsRoverGrid = new MarsRoverGrid();
			_marsRoverGrid.SetMarsRoverGridBoundary(5,5);
			_marsRoverGrid.SetRoverStartPosition(3, 3);
			var rover = new Rover.Core.Entities.Rover() {GridPosition = new YAndXCoordinate {X = 2, Y = 2}};

			Assert.Throws<GridSpaceOccupiedException>(() => _marsRoverGrid.MoveRoverTo(rover, 3, 3));
		}
	}
}