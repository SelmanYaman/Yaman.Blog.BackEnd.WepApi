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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseIdentityColumn();
            builder.Property(I => I.AuthorName).HasMaxLength(50).IsRequired();
            builder.Property(I => I.AuthorEmail).HasMaxLength(50).IsRequired();
            builder.Property(I => I.Description).HasColumnType("ntext").IsRequired();

            builder.HasOne(I => I.ParentComment).WithMany(I => I.SubComments).HasForeignKey(I => I.ParentCommentId);
        }
    }
}
