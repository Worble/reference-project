using System;

namespace Forum.Application.Forum.Commands.CreatePost
{
	[Serializable]
	public class CreatePostCommandException : ApplicationLayerException
	{
		public CreatePostCommandException() : base()
		{
		}

		public CreatePostCommandException(string message) : base(message)
		{
		}

		public CreatePostCommandException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
