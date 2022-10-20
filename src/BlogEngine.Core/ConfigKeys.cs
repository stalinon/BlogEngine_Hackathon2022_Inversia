namespace BlogEngine.Core;

/// <summary>
///     Ключи конфигурации
/// </summary>
public static class ConfigKeys
{
    /// <summary>
    ///     Строка подключения к БД
    /// </summary>
    public const string DB_CONNECTION_STRING = nameof(DB_CONNECTION_STRING);

    /// <summary>
    ///     Логин администратора
    /// </summary>
    public const string ADMIN_LOGIN = nameof(ADMIN_LOGIN);

    /// <summary>
    ///     Пароль администратора
    /// </summary>
    public const string ADMIN_PWD = nameof(ADMIN_PWD);
}
