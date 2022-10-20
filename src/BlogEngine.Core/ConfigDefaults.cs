namespace BlogEngine.Core;

/// <summary>
///     Конфигурация по умолчанию
/// </summary>
public static class ConfigDefaults
{
    /// <summary>
    ///     Строка подключения к БД
    /// </summary>
    public const string DB_CONNECTION_STRING 
        = "Server=127.0.0.1;User Id=postgres;Port=35432;Database=__engine;Password=1234;Include Error Detail=true;Pooling=false;Timeout=1024;Command Timeout=1024;";

    /// <summary>
    ///     Логин администратора
    /// </summary>
    public const string ADMIN_LOGIN = "admin";

    /// <summary>
    ///     Пароль администратора
    /// </summary>
    public const string ADMIN_PWD = "1234";
}
