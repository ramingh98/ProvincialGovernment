using DataModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class StudyFieldMap : IEntityTypeConfiguration<StudyFieldModel>
    {
        public void Configure(EntityTypeBuilder<StudyFieldModel> builder)
        {
            builder.ToTable("StudyField");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd();
            builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
