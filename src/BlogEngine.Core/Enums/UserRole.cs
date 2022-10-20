using System.Runtime.Serialization;

namespace BlogEngine.Core.Enums;

/// <summary>
///     Роль пользователя
/// </summary>
public enum UserRole
{
    [EnumMember(Value = "ADMIN")]
    ADMIN,
    [EnumMember(Value = "USER")]
    USER,
    [EnumMember(Value = "UNAUTHORIZED")]
    UNAUTHORIZED
}
