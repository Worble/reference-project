using System;

namespace Forum.Application.Forum.Queries.Login
{
	[Serializable]
	public class LoginFailedException : ApplicationLayerException
	{
		public LoginFailedException() : base()
		{
		}

		public LoginFailedException(string message) : base(message)
		{
		}

		public LoginFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
