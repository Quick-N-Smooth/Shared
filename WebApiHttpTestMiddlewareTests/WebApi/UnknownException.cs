﻿namespace WebApi
{
    public class UnknownException : Exception
    {
        public UnknownException() :base() { }
        
        public UnknownException(string message) : base(message) { }
    }
}
