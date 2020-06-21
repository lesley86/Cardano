using System;

namespace Cardano.Rover.Core.Exceptions
{
	public class InvalidCoordinateException : Exception
	{

		public InvalidCoordinateException(string msg)
			:base(msg)
		{
			
		}
		
	}
}