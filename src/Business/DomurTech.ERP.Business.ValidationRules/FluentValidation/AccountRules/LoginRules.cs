using DomurTech.ERP.Business.Entities.Models.AccountModels;
using DomurTech.Globalization;
using FluentValidation;

namespace DomurTech.ERP.Business.ValidationRules.FluentValidation.AccountRules
{
    public class LoginRules : AbstractValidator<LoginModel>
    {
        public LoginRules()
        {
            RuleFor(p => p.Username).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Username));
            RuleFor(p => p.Password).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Password));
        }
    }
}
