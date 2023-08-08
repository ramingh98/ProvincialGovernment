using DataModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class TypeOfEmploymentMap : IEntityTypeConfiguration<TypeOfEmploymentModel>
    {
        public void Configure(EntityTypeBuilder<TypeOfEmploymentModel> builder)
        {
            builder.ToTable("TypeOfEmployment");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd();
            builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
