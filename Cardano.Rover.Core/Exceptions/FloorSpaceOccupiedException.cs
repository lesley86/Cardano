using System;

namespace Cardano.Rover.Core.Exceptions
{
	public class FloorSpaceOccupiedException : Exception
	{

		public FloorSpaceOccupiedException(string msg)
			:base(msg)
		{
			
		}
	}
}