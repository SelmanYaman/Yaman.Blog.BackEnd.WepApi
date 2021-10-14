using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yaman.Blog.BackEnd.DataAcess.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Entities.Concrete.Blog>
    {
        public void Configure(EntityTypeBuilder<Entities.Concrete.Blog> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.Title).HasMaxLength(100).IsRequired();
            builder.Property(I => I.ShortDescription).HasMaxLength(500).IsRequired();
            builder.Property(I => I.Description).HasColumnType("ntext").IsRequired();
            builder.Property(I => I.ImagePath).HasMaxLength(200).IsRequired();

            builder.HasMany(I => I.Comments).WithOne(I => I.Blog).HasForeignKey(I => I.BlogId);
        }
    }
}
