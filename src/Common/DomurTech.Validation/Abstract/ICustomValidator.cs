using System.Collections.Generic;
using DomurTech.Validation.Entities;

namespace DomurTech.Validation.Abstract
{
    public interface ICustomValidator
    {
        bool IsValid { get; set; }

        List<CustomValidationResult> Validate();
    }
}
