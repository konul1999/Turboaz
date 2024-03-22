﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurboAz.Models.Auditable;

namespace TurboAz.Models.Configurations
{
    static class ConfigurationsHelper
    {
        internal static EntityTypeBuilder<T> ConfigureAuditable<T>(this EntityTypeBuilder<T> builder)
            where T : AuditableEntity
        {
            builder.Property(m => m.CreatedBy).HasColumnType("int").IsRequired();
            builder.Property(m => m.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(m => m.LastModifiedBy).HasColumnType("int");
            builder.Property(m => m.LastModifiedAt).HasColumnType("datetime");
            builder.Property(m => m.DeletedBy).HasColumnType("int");
            builder.Property(m => m.DeletedAt).HasColumnType("datetime");

            return builder;
        }
    }
}
