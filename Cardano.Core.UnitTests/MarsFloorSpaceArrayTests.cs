using Cardano.Rover.Core;
using NUnit.Framework;

namespace Cardano.Core.UnitTests
{
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
	}
}