﻿using System;

namespace DomurTech.Exceptions.Abstract
{
    public abstract class BaseApplicationException : ApplicationException
    {
        protected BaseApplicationException()
        {

        }

        protected BaseApplicationException(string message) : base(message)
        {

        }

        protected BaseApplicationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
