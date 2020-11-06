using System;
using System.Runtime.Serialization;

namespace Forum.Domain.Forum.Posts
{
	[Serializable]
	public class PostException : DomainLayerException
	{
		public PostException()
		{
		}

		public PostException(string message) : base(message)
		{
		}

		public PostException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected PostException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
