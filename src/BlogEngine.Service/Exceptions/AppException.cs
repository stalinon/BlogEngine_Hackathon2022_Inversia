using System.Globalization;
using System.Net;

namespace BlogEngine.Service.Exceptions;

/// <summary>
///     Ошибка авторизации
/// </summary>
public class AppException : Exception
{
    public HttpStatusCode Code { get; }

    /// <inheritdoc cref="AppException"/>
    public AppException() : base() { }

    /// <inheritdoc cref="AppException"/>
    public AppException(string message, HttpStatusCode statusCode) : base(message) 
        => Code = statusCode;

    /// <inheritdoc cref="AppException"/>
    public AppException(string message, HttpStatusCode statusCode, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args)) 
        => Code = statusCode;
}
