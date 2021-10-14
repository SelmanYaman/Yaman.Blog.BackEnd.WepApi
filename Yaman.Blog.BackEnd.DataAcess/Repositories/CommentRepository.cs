using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.DataAcess.Context;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.DataAcess.Repositories
{
    public class CommentRepository : Repository<Comment>,ICommentRepository
    {
        private readonly BlogContext _blogContext;
        public CommentRepository(BlogContext blogContext) : base(blogContext)
        {
            _blogContext = blogContext;
        }

        public async Task<List<Comment>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            List<Comment> result = new();
            await GetComments(blogId, parentId, result);
            return result;
        }

        private async Task GetComments(int blogId, int? parentId, List<Comment> result)
        {
            var comments = await _blogContext.Set<Comment>().Where(I => I.BlogId == blogId && I.ParentCommentId == parentId && I.IsApproved == true).OrderByDescending(I => I.PostedTime).ToListAsync();
            if (comments.Count > 0)
            {
                foreach (var comment in comments)
                {
                    if (comment.SubComments == null)
                        comment.SubComments = new List<Comment>();

                    await GetComments(comment.BlogId, comment.Id, comment.SubComments);

                    if (!result.Contains(comment))
                    {
                        result.Add(comment);
                    }
                }
            }
        }
    }
}
