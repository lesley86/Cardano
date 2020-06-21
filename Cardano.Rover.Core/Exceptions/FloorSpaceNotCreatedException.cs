using System;

namespace Cardano.Rover.Core.Exceptions
{
	public class FloorSpaceNotCreatedException : Exception
	{

		public FloorSpaceNotCreatedException(string msg)
			:base(msg)
		{
			
		}
	}
}