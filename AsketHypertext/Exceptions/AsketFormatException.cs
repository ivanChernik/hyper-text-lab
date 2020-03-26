using System;

namespace AsketHypertext.Exceptions
{
    public class AsketFormatException : Exception
    {
        public AsketFormatException() : base("An error occured while parsing Asket page.")
        {
        }
    }
}
