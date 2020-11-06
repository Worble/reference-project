using System;

namespace Forum.Domain.Forum.Users
{
	[Serializable]
	public class UserException : DomainLayerException
	{
		public UserException()
		{
		}

		public UserException(string message) : base(message)
		{
		}

		public UserException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
