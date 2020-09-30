using System;
using System.Runtime.Serialization;

namespace Forum.Domain.Forum.Topics
{
	[Serializable]
	public class TopicException : DomainLayerException
	{
		public TopicException()
		{
		}

		public TopicException(string message) : base(message)
		{
		}

		public TopicException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected TopicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
