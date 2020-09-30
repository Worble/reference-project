using System;

namespace Forum.Application
{
	[Serializable]
	public class ApplicationLayerException : Exception
	{
		public ApplicationLayerException()
		{
		}

		public ApplicationLayerException(string message) : base(message)
		{
		}

		public ApplicationLayerException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
