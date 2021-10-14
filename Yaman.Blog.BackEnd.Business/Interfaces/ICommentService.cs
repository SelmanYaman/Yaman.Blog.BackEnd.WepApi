using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.CommentDtos;
using Yaman.Blog.BackEnd.Dtos.Interfaces;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Interfaces
{
    public interface ICommentService : IService<CommentCreateDto, CommentUpdateDto, CommentListDto, Comment>
    {
        Task<List<CommentListDto>> GetAllWithSubCommentsAsync(int blogId, int? parentId);
        Task<List<CommentListDto>> GetUnApprovedCommentsAsync();
    }
}
