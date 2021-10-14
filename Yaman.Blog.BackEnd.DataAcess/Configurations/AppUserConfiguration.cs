using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.DataAcess.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.UserName).HasMaxLength(20).IsRequired();
            builder.Property(I => I.Password).HasMaxLength(30).IsRequired();
            builder.Property(I => I.Name).HasMaxLength(25).IsRequired();
            builder.Property(I => I.SurName).HasMaxLength(25).IsRequired();
            builder.Property(I => I.Email).HasMaxLength(50).IsRequired();
            builder.Property(I => I.PhoneNumber).HasMaxLength(25).IsRequired();
            builder.Property(I => I.Adress).HasMaxLength(250).IsRequired();
            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "Admin",
                Password = "123",
                Name = "Junior",
                SurName = "Developer",
                Email = "developer@gmail.com",
                PhoneNumber = "0850",
                Adress = "TÜRKİYE"
            });

            builder.HasMany(I => I.Blogs).WithOne(I => I.AppUser).HasForeignKey(I => I.AppUserId);

        }
    }
}
