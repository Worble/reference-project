using System;

namespace Forum.Application.Common.Users
{
	[Serializable]
	public class UserEntityException : ApplicationLayerException
	{
		public UserEntityException()
		{
		}

		public UserEntityException(string message) : base(message)
		{
		}

		public UserEntityException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
