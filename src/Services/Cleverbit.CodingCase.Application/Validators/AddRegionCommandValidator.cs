using Cleverbit.CodingCase.Application.Requests.Commands;
using FluentValidation;

namespace Cleverbit.CodingCase.Application.Validators;
public class AddRegionCommandValidator : AbstractValidator<AddRegionCommand>
{
    public AddRegionCommandValidator()
    {
    }
}