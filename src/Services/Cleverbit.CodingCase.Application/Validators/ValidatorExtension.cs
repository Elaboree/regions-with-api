using FluentValidation;

namespace Cleverbit.CodingCase.Application.Validators;
public static class ValidatorExtension
{
    public static IRuleBuilderOptions<T, string> EmployeeNameAndSurnameValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .NotEmpty()
            .WithErrorCode(ValidationErrorCode.InvalidEmployeeNameOrSurname.GenerateValidationErrorCode())
            .WithMessage(ValidationErrorCode.InvalidEmployeeNameOrSurname.GenerateValidationErrorMessage());
    }

    public static IRuleBuilderOptions<T, int> RegionIdValidator<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThan(0)
            .LessThanOrEqualTo(1000)
            .WithErrorCode(ValidationErrorCode.InvalidRegionId.GenerateValidationErrorCode())
            .WithMessage(ValidationErrorCode.InvalidRegionId.GenerateValidationErrorMessage());
    }
}