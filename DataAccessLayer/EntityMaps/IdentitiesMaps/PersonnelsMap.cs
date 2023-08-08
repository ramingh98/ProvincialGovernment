using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.EntityMaps.IdentitiesMaps
{
    public class PersonnelsMap : IEntityTypeConfiguration<PersonnelsModel>
    {
        public void Configure(EntityTypeBuilder<PersonnelsModel> builder)
        {
            builder.ToTable("Personnels");
            builder.HasKey(q => q.Id);
            builder.HasIndex(q => q.NationalCode).IsUnique();
            builder.Property(q => q.Id).ValueGeneratedOnAdd().HasColumnType("bigint").IsRequired();
            builder.Property(q => q.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(q => q.Family).HasColumnType("nvarchar(70)").IsRequired();
            builder.Property(q => q.NationalCode).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(q => q.BirthCertificateNumber).HasColumnType("nvarchar(10)").IsRequired();
            builder.Property(q => q.PlaceOfBirth).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(q => q.ComputerCode).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(q => q.CaseNumber).HasColumnType("bigint").IsRequired();
            builder.Property(q => q.BirthDate).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(q => q.RegDate).HasColumnType("datetime").IsRequired();
            builder.HasOne(q => q.EducationDegree).WithMany(q => q.Personnels).HasForeignKey(c => c.EducationDegreeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.StudyField).WithMany(q => q.Personnels).HasForeignKey(q => q.StudyFieldId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.TypeOfPosition).WithMany(q => q.Personnels).HasForeignKey(q => q.LastPositionId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.ServiceLocation).WithMany(q => q.Personnels).HasForeignKey(q => q.ServiceLocationId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.MaritalStatus).WithMany(q => q.Personnels).HasForeignKey(q => q.MaritalStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.CaseStatus).WithMany(q => q.Personnels).HasForeignKey(q => q.CaseStatusId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.TypeOfEmployment).WithMany(q => q.Personnels).HasForeignKey(q => q.TypeOfEmploymentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
