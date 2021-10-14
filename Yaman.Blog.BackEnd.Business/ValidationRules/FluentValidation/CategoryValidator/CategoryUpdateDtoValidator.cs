using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.CategoryDtos;

namespace Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.CategoryValidator
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(I => I.Id).NotEmpty();
            RuleFor(I => I.Name).NotEmpty().WithMessage("Kategori Boş Geçilemez");
        }
    }
}
