using System;
using Audit.EntityFramework;

namespace Forum.Application.Common.Audit
{
	[AuditIgnore]
	public class AuditLog
	{
		public int Id { get; set; }
		public object? AuditData { get; set; }
		public string EntityType { get; set; } = string.Empty;
		public DateTime AuditDate { get; set; }
		public string EntityPrimaryKey { get; set; } = string.Empty;
	}
}
