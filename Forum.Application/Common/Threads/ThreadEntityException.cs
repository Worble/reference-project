using System;

namespace Forum.Application.Common.Threads
{
	[Serializable]
	public class ThreadEntityException : Exception
	{
		public ThreadEntityException()
		{
		}

		public ThreadEntityException(string message) : base(message)
		{
		}

		public ThreadEntityException(string message, Exception innerException) :
			base(message, innerException)
		{
		}
	}
}
