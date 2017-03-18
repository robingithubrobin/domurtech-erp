using DomurTech.ERP.UI.Web.Installation.Models;
using DomurTech.Globalization;
using FluentValidation;

namespace DomurTech.ERP.UI.Web.Installation.ValidationRules
{
    public class SettingRules : AbstractValidator<SettingModel>
    {
        public SettingRules()
        {
            RuleFor(p => p.ApplicationUrl).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Url));
            RuleFor(p => p.ApplicationName).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.ApplicationName));
            RuleFor(p => p.SmtpServer).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, "SMTP Sunucu"));
            RuleFor(p => p.SmtpPort).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, "SMTP Port"));
            RuleFor(p => p.SmtpSsl).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, "SSL"));
            RuleFor(p => p.SmtpUser).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Username));
            RuleFor(p => p.SmtpPassword).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Password));
            RuleFor(p => p.SmtpSenderName).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, "Görünen Ad"));
            RuleFor(p => p.SmtpSenderMail).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Email));
            RuleFor(p => p.SmtpSenderMail).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Email));
        }
    }
}