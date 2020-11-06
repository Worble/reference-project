namespace Forum.Application.Models.RequestPerformanceConfigurations
{
	public class RequestPerformanceConfiguration
	{
		public bool Enabled { get; set; }
		public long? WarningLogTimeMilliseconds { get; set; }
		public bool LogAllTimes { get; set; }
	}
}
