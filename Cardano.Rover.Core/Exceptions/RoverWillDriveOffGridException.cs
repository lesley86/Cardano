using System;

namespace Cardano.Rover.Core.Exceptions
{
	public class RoverWillDriveOffGridException : Exception
	{

		public RoverWillDriveOffGridException(string msg)
			:base(msg)
		{
			
		}
		
	}
}