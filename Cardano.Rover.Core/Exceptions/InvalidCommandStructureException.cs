using System;

namespace Cardano.Rover.Core.Exceptions
{
	public class InvalidCommandStructureException : Exception
	{

		public InvalidCommandStructureException(string msg)
			:base(msg)
		{
			
		}
	}
}