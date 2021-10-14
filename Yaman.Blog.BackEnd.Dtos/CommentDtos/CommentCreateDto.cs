using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.Interfaces;

namespace Yaman.Blog.BackEnd.Dtos.CommentDtos
{
    public class CommentCreateDto : IDto
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string Description { get; set; }
        public DateTime PostedTime { get; set; } 
        public bool IsApproved { get; set; }
        public int? ParentCommentId { get; set; }
        public int BlogId { get; set; }
    }
}
