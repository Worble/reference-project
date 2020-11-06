using System;

namespace Forum.Application.Abstractions.Dates
{
	public interface IDateTimeService
	{
		DateTime UtcNow { get; }
		DateTimeOffset UtcNowOffset { get; }
	}
}
