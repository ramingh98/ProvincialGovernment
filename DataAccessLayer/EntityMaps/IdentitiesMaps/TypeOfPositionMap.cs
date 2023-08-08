using DataModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class TypeOfPositionMap : IEntityTypeConfiguration<TypeOfPositionModel>
    {
        public void Configure(EntityTypeBuilder<TypeOfPositionModel> builder)
        {
            builder.ToTable("TypeOfPosition");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd();
            builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(60)");
        }
    }
}
