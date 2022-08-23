using System;

namespace Bussines.Abstraction.Exceptions
{
    public class EntityNullReferenceException : Exception
    {
        public EntityNullReferenceException(string message) : base(message) 
        {
        }
    }
}
