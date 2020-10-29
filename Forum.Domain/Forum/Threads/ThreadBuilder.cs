using System;
using System.Collections.Generic;
using Forum.Domain.Abstractions.Models;
using Forum.Domain.Forum.Posts;
using Forum.Domain.Forum.Topics;
using Forum.Domain.Forum.Users;

namespace Forum.Domain.Forum.Threads
{
	public class ThreadBuilder : IBuilder<Thread>
	{
		private readonly Thread _thread;
		private PostBuilder? _postBuilder;

		public ThreadBuilder()
		{
			_thread = new Thread();
		}

		public Thread Build()
		{
			if (_postBuilder == null)
			{
				throw new ThreadException("No post associated with thread");
			}

			_thread.Posts = new List<Post> {_postBuilder.Build()};
			_thread.Validate();
			_thread.AddDomainEvent(new ThreadCreatedEvent(_thread));
			return _thread;
		}

		public ThreadBuilder InTopic(Topic topic)
		{
			_thread.Topic = topic ?? throw new ArgumentNullException(nameof(topic));
			return this;
		}

		public ThreadBuilder WithPost(PostBuilder postBuilder)
		{
			if (postBuilder == null)
			{
				throw new ArgumentNullException(nameof(postBuilder));
			}

			postBuilder.InThread(_thread);
			_postBuilder = postBuilder;
			return this;
		}

		public ThreadBuilder CreatedBy(User user)
		{
			_thread.CreatedBy = user ?? throw new ArgumentNullException(nameof(user));
			return this;
		}

		public ThreadBuilder CreatedDate(DateTime createdDate)
		{
			_thread.CreatedDate = createdDate;
			return this;
		}

		public ThreadBuilder WithTitle(string title)
		{
			_thread.Title = title ?? throw new ArgumentNullException(nameof(title));
			return this;
		}
	}
}
