using FluentValidation;
using ThomasGregChallenge.Application.DTOs.Requests;

namespace ThomasGregChallenge.Application.Validators
{
    public class ClienteRequestDtoValidator : AbstractValidator<ClienteRequestDto>
    {
        public ClienteRequestDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome não pode ser vazio")
                .MaximumLength(150)
                .WithMessage("Nome deve ter no máximo 150 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail não pode ser vazio")
                .MaximumLength(150)
                .WithMessage("E-mail deve ter no máximo 150 caracteres");

            RuleFor(x => x.Logotipo)
                .NotEmpty()
                .WithMessage("Logotipo não pode ser vazio")
                .MaximumLength(150)
                .WithMessage("Logotipo deve ter no máximo 150 caracteres");
        }
    }
}
