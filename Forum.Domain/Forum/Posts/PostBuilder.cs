using System;
using System.Threading.Tasks;
using Forum.Domain.Abstractions.Models;
using Forum.Domain.Forum.Threads;
using Forum.Domain.Forum.Users;

namespace Forum.Domain.Forum.Posts
{
	public class PostBuilder : IBuilder<Post>
	{
		private readonly Post _post;

		public PostBuilder()
		{
			_post = new Post();
		}

		public async Task<Post> Build()
		{
			_post.Validate();
			_post.AddDomainEvent(new PostCreatedEvent(_post));
			await _post
				.DispatchDomainEventsAsync()
				.ConfigureAwait(false);
			return _post;
		}

		public PostBuilder CreatedByUser(User user)
		{
			_post.CreatedBy = user ?? throw new ArgumentNullException(nameof(user));
			return this;
		}

		public PostBuilder CreatedDateUtc(DateTime createdDate)
		{
			_post.CreatedDateUtc = createdDate;
			return this;
		}

		public PostBuilder InThread(Thread thread)
		{
			_post.Thread = thread ?? throw new ArgumentNullException(nameof(thread));
			return this;
		}

		public PostBuilder WithContent(string content)
		{
			_post.Content = content ?? throw new ArgumentNullException(nameof(content));
			return this;
		}
	}
}
