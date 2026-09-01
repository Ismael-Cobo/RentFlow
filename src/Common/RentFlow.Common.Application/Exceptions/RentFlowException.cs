using RentFlow.Common.Domain;

namespace RentFlow.Common.Application.Exceptions;

public sealed class RentFlowException : Exception
{
    public RentFlowException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
