using DataModels.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class ServiceLocationMap : IEntityTypeConfiguration<ServiceLocationModel>
    {
        public void Configure(EntityTypeBuilder<ServiceLocationModel> builder)
        {
            builder.ToTable("ServiceLocation");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.Id);
            builder.Property(q => q.Id).ValueGeneratedOnAdd();
            builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(100)");
        }
    }
}
