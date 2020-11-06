using System;
using System.Runtime.Serialization;

namespace Forum.Domain.Forum.SubForums
{
	[Serializable]
	public class SubForumException : DomainLayerException
	{
		public SubForumException()
		{
		}

		public SubForumException(string message) : base(message)
		{
		}

		public SubForumException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected SubForumException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
