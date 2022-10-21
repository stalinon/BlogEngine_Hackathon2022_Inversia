using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BlogEngine.Service.Configuration;

/// <summary>
///     Опции авторизации
/// </summary>
public class AuthOptions
{
    /// <summary>
    ///     Издатель
    /// </summary>
    public const string ISSUER = "MyAuthServer";

    /// <summary>
    ///     Потребитель
    /// </summary>
    public const string AUDIENCE = "MyAuthClient";

    /// <summary>
    ///     Секретный ключ
    /// </summary>
    const string KEY = "mysupersecret_secretkey!123";

    /// <summary>
    ///     Получить симметричный ключ безопасности
    /// </summary>
    /// <returns></returns>
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new(Encoding.UTF8.GetBytes(KEY));
}
