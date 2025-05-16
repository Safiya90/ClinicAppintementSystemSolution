using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicAppintementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAppintementSystem.Configuration
{
  public class AppointementConfiguration : IEntityTypeConfiguration<Appointement>
    {
        public void Configure(EntityTypeBuilder<Appointement> builder)
        {
            builder.HasKey(a => a.AppointementId);
            builder.Property(a => a.AppointementId)
                .ValueGeneratedOnAdd();
            builder.Property(a => a.Date)
                .IsRequired();
            builder.HasOne(a => a.doctor)
                .WithMany(a => a.Appointements)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a=>a.patient)
                .WithMany(a=>a.Appointements)
                .HasForeignKey(a=>a.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
