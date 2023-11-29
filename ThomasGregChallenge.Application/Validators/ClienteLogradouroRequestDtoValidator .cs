using FluentValidation;
using ThomasGregChallenge.Application.DTOs.Requests;

namespace ThomasGregChallenge.Application.Validators
{
    public class ClienteLogradouroRequestDtoValidator : AbstractValidator<ClienteLogradouroRequestDto>
    {
        public ClienteLogradouroRequestDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("Nome não pode ser vazio e deve ter no máximo 150 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("E-mail não pode ser vazio e deve ter no máximo 150 caracteres");

            RuleFor(x => x.Logotipo)                
                .NotEmpty()
                .MaximumLength(150)
                .WithMessage("Logotipo não pode ser vazio e deve ter no máximo 150 caracteres");

            RuleForEach(x => x.Logradouros)
                .SetValidator(new LogradouroRequestDtoValidator());
        }
    }
}