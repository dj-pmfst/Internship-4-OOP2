using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration.Users
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("id");

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(User.FirstNameMaxLength)
                .HasColumnName("name");

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(User.SurnameMaxLength)
                .HasColumnName("surname");

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("username");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("email");

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("password");

            builder.Property(u => u.adressStreet)
                .IsRequired()
                .HasMaxLength(User.StreetMaxLength)
                .HasColumnName("address_street");

            builder.Property(u => u.adressCity)
                .IsRequired()
                .HasMaxLength(User.CityMaxLength)
                .HasColumnName("address_city");

            builder.Property(u => u.website)
                .HasMaxLength(User.WebsiteMaxLength)
                .HasColumnName("website");

        }
    }
}
