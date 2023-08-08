using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    internal class MaritalStatusMap : IEntityTypeConfiguration<MaritalStatusModel>
    {
        public void Configure(EntityTypeBuilder<MaritalStatusModel> builder)
        {
            builder.ToTable("MaritalStatus");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedNever();
            builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(30)");
        }
    }
}
