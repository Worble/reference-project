using System;

namespace Forum.Application.Common.Topics
{
	[Serializable]
	public class TopicEntityException : Exception
	{
		public TopicEntityException()
		{
		}

		public TopicEntityException(string message) : base(message)
		{
		}

		public TopicEntityException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
