using System;
using Forum.Application.Abstractions.Dates;

namespace Forum.Infrastructure.Dates
{
	public class DateTimeService : IDateTimeService
	{
		public DateTime UtcNow => DateTime.UtcNow;
		public DateTimeOffset UtcNowOffset => DateTimeOffset.UtcNow;
	}
}
