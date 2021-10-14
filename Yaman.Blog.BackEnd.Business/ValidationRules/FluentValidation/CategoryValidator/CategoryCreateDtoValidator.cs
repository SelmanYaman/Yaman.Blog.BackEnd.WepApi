using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.CategoryDtos;

namespace Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.CategoryValidator
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(I => I.Name).NotEmpty().WithMessage("Kategori Boş Geçilemez");
        }
    }
}
