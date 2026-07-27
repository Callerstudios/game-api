using FluentValidation;
using GameApi.DTOs.Players;

namespace GameApi.Validators;

public class CreatePlayerDtoValidator
    : AbstractValidator<CreatePlayerDto>
{
    public CreatePlayerDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")

            .MaximumLength(20)
            .WithMessage("Username cannot exceed 20 characters.");
    }

}