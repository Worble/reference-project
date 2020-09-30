using System;

namespace Forum.Application.Common.Posts
{
	[Serializable]
	public class PostEntityException : ApplicationLayerException
	{
		public PostEntityException()
		{
		}

		public PostEntityException(string message) : base(message)
		{
		}

		public PostEntityException(string message, Exception innerException) :
			base(message, innerException)
		{
		}
	}
}
