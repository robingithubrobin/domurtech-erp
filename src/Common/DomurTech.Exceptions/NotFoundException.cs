using System;
using DomurTech.Exceptions.Abstract;
using DomurTech.Globalization;

namespace DomurTech.Exceptions
{
    [Serializable]
    public class NotFoundException : BaseApplicationException
    {

        public NotFoundException() : base(Messages.DangerRecordNotFound)
        {

        }

        public NotFoundException(string message) : base(message)
        {

        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
