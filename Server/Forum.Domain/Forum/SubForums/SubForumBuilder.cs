using System;
using Forum.Domain.Abstractions.Models;

namespace Forum.Domain.Forum.SubForums
{
	public class SubForumBuilder : IBuilder<SubForum>
	{
		private readonly SubForum _subForum;

		public SubForumBuilder()
		{
			_subForum = new SubForum();
		}

		public SubForum Build()
		{
			_subForum.Validate();
			_subForum.AddDomainEvent(new SubForumCreatedEvent(_subForum));
			return _subForum;
		}

		public SubForumBuilder WithName(string name)
		{
			_subForum.Title = name ?? throw new ArgumentNullException(nameof(name));
			return this;
		}
	}
}
