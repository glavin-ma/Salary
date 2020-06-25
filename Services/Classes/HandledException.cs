using System;

namespace Services.Classes
{
    public class HandledException : Exception
    {
        public HandledException(string message) : base(message) { }
    }
}
