using Alef_Vinal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Contexts.ModelConfigurations
{
    public class CodeEntityConfiguration : IEntityTypeConfiguration<CodeEntity>
    {
        public void Configure(EntityTypeBuilder<CodeEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(3);
        }
    }
}
