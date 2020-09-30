namespace Forum.Application.Models.RequestPerformanceConfigurations
{
	public class RequestPerformanceConfiguration
	{
		public bool Enabled { get; }
		public int? WarningLogTimeMilliseconds { get; }
		public bool LogAllTimes { get; }
	}
}
