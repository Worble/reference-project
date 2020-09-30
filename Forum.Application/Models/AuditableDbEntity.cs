using System;
using Forum.Application.Abstractions.Models;

namespace Forum.Application.Models
{
	public class AuditableDbEntity : IDbEntity
	{
		public string AuditCreatedById { get; set; } = string.Empty;
		public string AuditCreatedByName { get; set; } = string.Empty;
		public DateTime AuditCreatedDateUtc { get; set; }
		public string? AuditLastModifiedById { get; set; }
		public string? AuditLastModifiedByName { get; set; }
		public DateTime? AuditLastModifiedUtc { get; set; }
	}
}
