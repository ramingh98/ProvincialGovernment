using DataModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class CaseStatusMap : IEntityTypeConfiguration<CaseStatusModel>
    {
        public void Configure(EntityTypeBuilder<CaseStatusModel> builder)
        {
            builder.ToTable("CaseStatus");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedNever();
            builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
