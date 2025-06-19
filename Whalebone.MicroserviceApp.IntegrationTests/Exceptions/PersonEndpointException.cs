using System;

namespace Whalebone.MicroserviceApp.IntegrationTests.Exceptions;

public class PersonEndpointException : Exception
{
    public PersonEndpointException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
