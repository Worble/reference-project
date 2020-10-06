using System;
using Forum.Domain.Models;

namespace Forum.Application.Common.Forum.AuditEntries
{
	public class AuditEntry
	{
		public int Id { get; set; }
		public string CreatedById { get; set; } = string.Empty;
		public string CreatedByName { get; set; } = string.Empty;
		public DateTime CreateDate { get; set; }
		public string? EditedById { get; set; }
		public string? EditedByName { get; set; }
		public DateTime? EditedDate { get; set; }

		public DomainEntity? RelatedEntity { get; set; }
	}
}
