using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Functions
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }
}
