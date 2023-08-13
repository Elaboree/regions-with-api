using Cleverbit.CodingCase.Application.Constants;
using System.ComponentModel;

namespace Cleverbit.CodingCase.Application.Validators;
public enum ValidationErrorCode
{
    None = 0,

    [Description(ValidationErrorMessage.InvalidEmployeeNameOrSurname)]
    InvalidEmployeeNameOrSurname = 1,

    [Description(ValidationErrorMessage.InvalidRegionId)]
    InvalidRegionId = 2,

}

public static class ValidationExtension
{
    public static string GenerateValidationErrorMessage(this ValidationErrorCode statusCode)
    {
        return statusCode.GetDescription();
    }

    public static string GenerateValidationErrorCode(this ValidationErrorCode statusCode)
    {
        return ((int)statusCode).ToString();
    }
}
public static class EnumExtension
{
    public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            return null;
        }

        var description = enumValue.ToString();
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

        if (fieldInfo != null)
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (attrs != null && attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description;
    }
}
