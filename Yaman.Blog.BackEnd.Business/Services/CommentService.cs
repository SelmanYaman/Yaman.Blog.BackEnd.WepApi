using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Business.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.Interfaces;
using Yaman.Blog.BackEnd.DataAcess.UnitOfWork;
using Yaman.Blog.BackEnd.Dtos.CommentDtos;
using Yaman.Blog.BackEnd.Dtos.Interfaces;
using Yaman.Blog.BackEnd.Entities.Concrete;

namespace Yaman.Blog.BackEnd.Business.Services
{
    public class CommentService : Services<CommentCreateDto, CommentUpdateDto, CommentListDto, Comment>, ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IUow _uow;
        public CommentService(IMapper mapper, IValidator<CommentCreateDto> createDtoValidator, IValidator<CommentUpdateDto> updateDtoValidator, IUow uow, ICommentRepository commentRepository) : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<List<CommentListDto>> GetAllWithSubCommentsAsync(int blogId, int? parentId)
        {
            return _mapper.Map<List<CommentListDto>>(await _commentRepository.GetAllWithSubCommentsAsync(blogId, parentId));
        }

        public async Task<List<CommentListDto>> GetUnApprovedCommentsAsync()
        {
            return _mapper.Map<List<CommentListDto>>(await _uow.GetRepository<Comment>().GetAllAsync(I => I.IsApproved == false));
        }
    }
}
