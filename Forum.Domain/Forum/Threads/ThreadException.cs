using System;
using System.Runtime.Serialization;

namespace Forum.Domain.Forum.Threads
{
	[Serializable]
	public class ThreadException : DomainLayerException
	{
		public ThreadException()
		{
		}

		public ThreadException(string message) : base(message)
		{
		}

		public ThreadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ThreadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
