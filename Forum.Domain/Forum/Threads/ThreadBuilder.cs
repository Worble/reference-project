using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Domain.Abstractions.Models;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;

namespace Forum.Domain.Forum.Threads
{
	public class ThreadBuilder : IBuilder<Thread>
	{
		private readonly Thread _thread;

		public ThreadBuilder()
		{
			_thread = new Thread();
		}

		public async Task<Thread> Build()
		{
			_thread.Validate();
			_thread.AddDomainEvent(new ThreadCreatedEvent(_thread));
			await _thread
				.DispatchDomainEventsAsync()
				.ConfigureAwait(false);
			return _thread;
		}

		public ThreadBuilder InTopic(Topic topic)
		{
			_thread.Topic = topic ?? throw new ArgumentNullException(nameof(topic));
			return this;
		}

		public ThreadBuilder WithPost(Post post)
		{
			if (post == null)
			{
				throw new ArgumentNullException(nameof(post));
			}

			_thread.Posts = new List<Post> {post};
			return this;
		}

		public ThreadBuilder WithTitle(string title)
		{
			_thread.Title = title ?? throw new ArgumentNullException(nameof(title));
			return this;
		}
	}
}
