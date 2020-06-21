using System;

namespace Cardano.Rover.Core.Exceptions
{
	public class GridSpaceOccupiedException : Exception
	{

		public GridSpaceOccupiedException(string msg)
			:base(msg)
		{
			
		}
	}
}