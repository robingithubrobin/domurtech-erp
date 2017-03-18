using System;
using DomurTech.Exceptions.Abstract;
using DomurTech.Globalization;

namespace DomurTech.Exceptions
{
    public class DuplicateException : BaseApplicationException
    {
        public DuplicateException() : base(Messages.DangerDuplicatedRecord)
        {

        }

        public DuplicateException(string message) : base(message)
        {

        }

        public DuplicateException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
