using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClinicAppintementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicAppintementSystem.Configuration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
       public void Configure(EntityTypeBuilder<Doctor> builder)
        {

                builder.HasKey(d => d.DoctorId);
                builder.Property(d => d.DoctorId)
                    .ValueGeneratedOnAdd();
                builder.Property(d => d.DoctorName)
                    .IsRequired()
                    .HasMaxLength(100);
                builder.Property(d => d.Specialty)
                    .HasMaxLength(500)
                    .IsRequired();
                builder.Property(d => d.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(500);
            
        }
    }
}
