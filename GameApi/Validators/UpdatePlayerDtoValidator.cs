using FluentValidation;
using GameApi.DTOs.Players;

namespace GameApi.Validators;

public class UpdatePlayerDtoValidator
    : AbstractValidator<UpdatePlayerDto>
{
    public UpdatePlayerDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .MaximumLength(20);
    }
}