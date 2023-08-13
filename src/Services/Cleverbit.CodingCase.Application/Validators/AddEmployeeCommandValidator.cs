using Cleverbit.CodingCase.Application.Requests.Commands;
using FluentValidation;

namespace Cleverbit.CodingCase.Application.Validators;
public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
{
    public AddEmployeeCommandValidator()
    {
        RuleFor(employee => employee.Name).EmployeeNameAndSurnameValidator();
        RuleFor(employee => employee.Surname).EmployeeNameAndSurnameValidator();
        RuleFor(employee => employee.RegionId).RegionIdValidator();
    }
}