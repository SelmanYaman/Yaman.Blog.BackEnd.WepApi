using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaman.Blog.BackEnd.Dtos.AppUserDtos;

namespace Yaman.Blog.BackEnd.Business.ValidationRules.FluentValidation.AppUserValidator
{
    public class AppUserUpdateDtoValidator : AbstractValidator<AppUserUpdateDto>
    {
        public AppUserUpdateDtoValidator()
        {
            RuleFor(I => I.Id).NotEmpty();
            RuleFor(I => I.UserName).NotEmpty().WithMessage("Kullanıcı Adı Boş Geçilemez.");
            RuleFor(I => I.Password).NotEmpty().WithMessage("Şifre Boş Geçilemez.");
            RuleFor(I => I.Name).NotEmpty().WithMessage("İsim Boş Geçilemez.");
            RuleFor(I => I.SurName).NotEmpty().WithMessage("Soyisim Boş Geçilemez.");
            RuleFor(I => I.Email).NotEmpty().WithMessage("Email Boş Geçilemez.");
            RuleFor(I => I.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Boş Geçilemez.");
            RuleFor(I => I.Adress).NotEmpty().WithMessage("Adres Adı Boş Geçilemez.");
        }
    }
}
