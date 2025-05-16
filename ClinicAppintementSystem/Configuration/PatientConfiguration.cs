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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
       public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(p => p.PatientId);
            builder.Property(p => p.PatientId)
                .ValueGeneratedOnAdd();
            builder.Property(p => p.PatientName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.NationalId)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(p => p.PhoneNo)
                .HasMaxLength(20)
                .IsRequired();
        }
    
        
    }
}
