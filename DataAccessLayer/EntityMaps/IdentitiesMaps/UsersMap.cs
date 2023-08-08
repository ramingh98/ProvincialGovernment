using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class UsersMap : IEntityTypeConfiguration<UsersModel>
    {
        public void Configure(EntityTypeBuilder<UsersModel> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(q => q.Id);
            builder.Property(q=>q.UserName).HasColumnType("nvarchar(1000)").IsRequired();
            builder.Property(q => q.Password).HasColumnType("nvarchar(4000)").IsRequired();
        }
    }
}
