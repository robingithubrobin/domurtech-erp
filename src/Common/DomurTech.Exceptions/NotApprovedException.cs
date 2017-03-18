using System;
using DomurTech.Exceptions.Abstract;
using DomurTech.Globalization;

namespace DomurTech.Exceptions
{
    public class NotApprovedException : BaseApplicationException
    {
        public NotApprovedException() : base(Messages.DangerItemNotApproved)
        {

        }

        public NotApprovedException(string message) : base(message)
        {

        }

        public NotApprovedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
