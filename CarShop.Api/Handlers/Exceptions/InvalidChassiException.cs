﻿namespace CarShop.Api.Handlers.Exceptions
{
    internal sealed class InvalidChassiException : Exception
    {
        public InvalidChassiException(string message) : base(message)
        {

        }
    }
}
