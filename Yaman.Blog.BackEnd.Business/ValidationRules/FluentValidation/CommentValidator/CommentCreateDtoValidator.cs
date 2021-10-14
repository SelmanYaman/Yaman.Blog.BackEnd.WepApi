using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.CommentDtos;

namespace Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.CommentValidator
{
    public class CommentCreateDtoValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateDtoValidator()
        {
            RuleFor(I => I.AuthorName).NotEmpty().WithMessage("Rumuz Boş Geçilemez.");
            RuleFor(I => I.AuthorEmail).NotEmpty().WithMessage("Email Boş Geçilemez.");
            RuleFor(I => I.Description).NotEmpty().WithMessage("Açıklama Boş Geçilemez.");
        }
    }
}
