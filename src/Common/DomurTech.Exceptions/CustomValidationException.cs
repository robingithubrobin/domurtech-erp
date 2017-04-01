using System;
using System.Collections.Generic;
using DomurTech.Exceptions.Abstract;
using DomurTech.Validation.Entities;

namespace DomurTech.Exceptions
{
    [Serializable]
    public class CustomValidationException : BaseApplicationException
    {
        private List<CustomValidationResult> _validationResult;

        public List<CustomValidationResult> ValidationResult
        {
            get
            {
                return _validationResult ?? (_validationResult = new List<CustomValidationResult>());
            }
            set
            {
                _validationResult = value;
            }
        }

        public CustomValidationException()
        {

        }

        public CustomValidationException(string message) : base(message)
        {

        }

        public CustomValidationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
