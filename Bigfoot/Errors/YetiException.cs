using System;

namespace Bigfoot.Errors
{
    class YetiException : Exception
    {

        protected static string YetiErrorLabel = "Bigfoot :: ";

        public YetiException(string message) : base(message)
        {
        }
    }
}
