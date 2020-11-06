using System;
using System.Runtime.Serialization;

namespace Forum.Domain
{
	[Serializable]
	public class DomainLayerException : Exception
	{
		public DomainLayerException()
		{
		}

		public DomainLayerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public DomainLayerException(string message) : base(message)
		{
		}

		public DomainLayerException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
