using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Models.Entities;

namespace TurboAz.Models.Configurations
{
    class AnnouncementEntityTypeConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.Property(m => m.Id).HasColumnType("int");
            builder.Property(m => m.March).HasColumnType("int");
            builder.Property(m => m.Price).HasColumnType("decimal").HasPrecision(18,2);
            builder.Property(m => m.FuelType).HasColumnType("int").IsRequired();
            builder.Property(m => m.Banner).HasColumnType("int").IsRequired();
            builder.Property(m => m.GearBox).HasColumnType("int").IsRequired();
            builder.Property(m => m.Transmission).HasColumnType("int").IsRequired();
            builder.Property(m => m.ModelId).HasColumnType("int").IsRequired();
            builder.Property(m => m.Year).HasColumnType("int").IsRequired();
           

            builder.HasKey(m => m.Id);
            builder.HasKey(m => m.Id);
            builder.ToTable("Announcement");

            builder.HasOne<Model>()
                .WithMany()
                .HasForeignKey(m => m.ModelId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
