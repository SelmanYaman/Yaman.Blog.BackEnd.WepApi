using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.BlogDtos;

namespace Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.BlogValidator
{
    public class BlogCreateDtoValidator : AbstractValidator<BlogCreateDto>
    {
        public BlogCreateDtoValidator()
        {
            RuleFor(I => I.Title).NotEmpty().WithMessage("Başlık Boş Geçilemez.");
            RuleFor(I => I.ShortDescription).NotEmpty().WithMessage("Kısa Açıklama Boş Geçilemez.");
            RuleFor(I => I.Description).NotEmpty().WithMessage("Açıklama Boş Geçilemez.");
            RuleFor(I => I.CategoryId).NotEmpty().WithMessage("Kategori Seçiniz.");
        }
    }
}
