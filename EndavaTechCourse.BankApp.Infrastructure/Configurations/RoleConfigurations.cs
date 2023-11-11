﻿using EndavaTechCourse.BankApp.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EndavaTechCourse.BankApp.Infrastructure.Configurations
{
	public class RoleConfigurations : IEntityTypeConfiguration<IdentityRole<Guid>>
	{
		public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
		{
			var roles = Enum.GetNames(typeof(UserRole))
				.Select(role => new IdentityRole<Guid>() { Id = Guid.NewGuid(), Name = role, NormalizedName = role.Normalize() })
				.ToList();

			builder.HasData(roles);
		}
		
	}
}

