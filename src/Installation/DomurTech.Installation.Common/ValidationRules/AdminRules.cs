using DomurTech.Globalization;
using DomurTech.Helpers;
using DomurTech.Installation.Common.Models;
using FluentValidation;

namespace DomurTech.Installation.Common.ValidationRules
{
    public class AdminRules : AbstractValidator<AdminModel>
    {
        public AdminRules()
        {
            RuleFor(p => p.Username).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Username));
            RuleFor(p => p.Password).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Password));


            var t = RuleFor(p => p.Password).Must(password => SecurityHelper.ValidatePassword(password));
           t.WithMessage(Messages.DangerInvalidPassword);
            RuleFor(p => p.ConfirmPassword).Equal(p => p.Password).WithMessage(Messages.DangerPasswordsDoNotMatch);
            RuleFor(p => p.FirstName).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.FirstName));
            RuleFor(p => p.LastName).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.LastName));
            RuleFor(p => p.Email).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Email));
            RuleFor(p => p.Email).EmailAddress().WithMessage(string.Format(Messages.WarnInvalidField, Dictionaries.Email));
        }
    }
}