using DomurTech.ERP.UI.Web.Installation.Models;
using DomurTech.Globalization;
using FluentValidation;

namespace DomurTech.ERP.UI.Web.Installation.ValidationRules
{
    public class DatabaseConnectionRules : AbstractValidator<DatabaseConnectionModel>
    {
        public DatabaseConnectionRules()
        {
            RuleFor(p => p.DataSource).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Server));
            RuleFor(p => p.InitialCatalog).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Database));
            RuleFor(p => p.UserId).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Username));
            RuleFor(p => p.Password).NotEmpty().WithMessage(string.Format(Messages.DangerFieldIsEmpty, Dictionaries.Password));
        }
    }
}