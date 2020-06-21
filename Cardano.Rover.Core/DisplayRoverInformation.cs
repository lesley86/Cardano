using System;
using System.Text;

namespace Cardano.Rover.Core
{
	public class DisplayRoverInformation
	{
		private readonly IRoverProvider _roverProvider;

		public DisplayRoverInformation(IRoverProvider roverProvider)
		{
			_roverProvider = roverProvider;
		}
		
		public string GetRoverInformation()
		{
			var rovers = _roverProvider.GetAllRovers();
			var sb = new StringBuilder();
			foreach (var rover in rovers)
			{
				sb.Append($"" +
						$"Rover's y coordinate: {rover.GridPosition.Y}, " +
						$"Rover's x coordinate: {rover.GridPosition.X}, " +
						$"Rover's direction: {rover.RoverFacingDirection}, " +
						$"Rover's status: {rover.RoverStatus.ToString()}" +
						$"{Environment.NewLine}");
			}

			return sb.ToString();
		}

	}
}