using AutoFixture;
using Cardano.Rover.Core;
using Cardano.Rover.Core.Enums;
using NUnit.Framework;

namespace Cardano.Core.UnitTests
{
	[TestFixture]
	public class RoverActonToCoordinateTranslationServiceTests
	{
		private RoverActonToCoordinateTranslationService _roverActonToCoordinateTranslationService;
		private Fixture _fixture;
		
		[SetUp]
		public void Setup()
		{
			_fixture = new Fixture();
		}

		[Test]
		public void ShouldReturnCommandForRoverToPointWestIfFacingNorthAndTurningLeft()
		{
			var rover = _fixture.Build<Rover.Core.Entities.Rover>()
				.With(x => x.RoverFacingDirection, Direction.North)
				.Create();
			
			var roverActionCommand = new RoverActionCommand() {RoverAction = RoverAction.TurnLeft, ActionExecutionOrder = 1};
			_roverActonToCoordinateTranslationService = new RoverActonToCoordinateTranslationService(new RoverActionToDirection());

			var result = _roverActonToCoordinateTranslationService.Translate(rover, roverActionCommand);
			
			Assert.AreEqual(Direction.West,result.FacingDirection);
			Assert.AreEqual(rover.GridPosition.X,result.XCoordinate);
			Assert.AreEqual(rover.GridPosition.Y,result.YCoordinate);
		}
		
		[Test]
		public void ShouldReturnCommandForRoverToPointEastIfFacingNorthAndTurningRight()
		{
			var rover = _fixture.Build<Rover.Core.Entities.Rover>()
				.With(x => x.RoverFacingDirection, Direction.North)
				.Create();
			
			var roverActionCommand = new RoverActionCommand() {RoverAction = RoverAction.TurnRight, ActionExecutionOrder = 1};
			_roverActonToCoordinateTranslationService = new RoverActonToCoordinateTranslationService(new RoverActionToDirection());

			var result = _roverActonToCoordinateTranslationService.Translate(rover, roverActionCommand);
			
			Assert.AreEqual(Direction.East,result.FacingDirection);
			Assert.AreEqual(rover.GridPosition.X,result.XCoordinate);
			Assert.AreEqual(rover.GridPosition.Y,result.YCoordinate);
		}
		
		[Test]
		public void ShouldMoveRoverAlongYAccessIfFacingNorth()
		{
			var rover = _fixture.Build<Rover.Core.Entities.Rover>()
				.With(x => x.RoverFacingDirection, Direction.North)
				.Create();
			
			var roverActionCommand = new RoverActionCommand() {RoverAction = RoverAction.MoveForwardInTheDirectionRoverIsFacing, ActionExecutionOrder = 1};
			_roverActonToCoordinateTranslationService = new RoverActonToCoordinateTranslationService(new RoverActionToDirection());

			var result = _roverActonToCoordinateTranslationService.Translate(rover, roverActionCommand);
			
			Assert.AreEqual(Direction.North,result.FacingDirection);
			Assert.AreEqual(rover.GridPosition.X , result.XCoordinate);
			Assert.AreEqual(rover.GridPosition.Y-1 , result.YCoordinate);
		}
		
		[Test]
		public void ShouldMoveRoverAlongXAccessIfFacingWest()
		{
			var rover = _fixture.Build<Rover.Core.Entities.Rover>()
				.With(x => x.RoverFacingDirection, Direction.West)
				.Create();
			
			var roverActionCommand = new RoverActionCommand() {RoverAction = RoverAction.MoveForwardInTheDirectionRoverIsFacing, ActionExecutionOrder = 1};
			_roverActonToCoordinateTranslationService = new RoverActonToCoordinateTranslationService(new RoverActionToDirection());

			var result = _roverActonToCoordinateTranslationService.Translate(rover, roverActionCommand);
			
			Assert.AreEqual(Direction.West,result.FacingDirection);
			Assert.AreEqual(rover.GridPosition.X-1, result.XCoordinate);
			Assert.AreEqual(rover.GridPosition.Y, result.YCoordinate);
		}
		
		[Test]
		public void ShouldMoveRoverAlongYAccessIfFacingSouth()
		{
			var rover = _fixture.Build<Rover.Core.Entities.Rover>()
				.With(x => x.RoverFacingDirection, Direction.South)
				.Create();
			
			var roverActionCommand = new RoverActionCommand() {RoverAction = RoverAction.MoveForwardInTheDirectionRoverIsFacing, ActionExecutionOrder = 1};
			_roverActonToCoordinateTranslationService = new RoverActonToCoordinateTranslationService(new RoverActionToDirection());

			var result = _roverActonToCoordinateTranslationService.Translate(rover, roverActionCommand);
			
			Assert.AreEqual(Direction.South,result.FacingDirection);
			Assert.AreEqual(rover.GridPosition.X , result.XCoordinate);
			Assert.AreEqual(rover.GridPosition.Y+1, result.YCoordinate);
		}
		
		[Test]
		public void ShouldMoveRoverAlongXAccessIfFacingEast()
		{
			var rover = _fixture.Build<Rover.Core.Entities.Rover>()
				.With(x => x.RoverFacingDirection, Direction.East)
				.Create();
			
			var roverActionCommand = new RoverActionCommand() {RoverAction = RoverAction.MoveForwardInTheDirectionRoverIsFacing, ActionExecutionOrder = 1};
			_roverActonToCoordinateTranslationService = new RoverActonToCoordinateTranslationService(new RoverActionToDirection());

			var result = _roverActonToCoordinateTranslationService.Translate(rover, roverActionCommand);
			
			Assert.AreEqual(Direction.East,result.FacingDirection);
			Assert.AreEqual(rover.GridPosition.X+1, result.XCoordinate);
			Assert.AreEqual(rover.GridPosition.Y, result.YCoordinate);
		}
	}
}