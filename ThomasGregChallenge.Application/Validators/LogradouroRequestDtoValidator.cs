using FluentValidation;
using ThomasGregChallenge.Application.DTOs.Requests;

namespace ThomasGregChallenge.Application.Validators
{
    public class LogradouroRequestDtoValidator : AbstractValidator<LogradouroRequestDto>
    {
        public LogradouroRequestDtoValidator()
        {
            RuleFor(x => x.Bairro)
                .NotEmpty()
                .WithMessage("Bairro não pode ser vazio")
                .MaximumLength(100)
                .WithMessage("Bairro deve ter no máximo 100 caracteres");

            RuleFor(x => x.Cidade)
                .NotEmpty()
                .WithMessage("Cidade não pode ser vazio")
                .MaximumLength(100)
                .WithMessage("Cidade deve ter no máximo 100 caracteres");

            RuleFor(x => x.ClienteId)
                .NotEmpty()
                .WithMessage("ClienteId não pode ser vazio")
                .GreaterThan(0)
                .WithMessage("ClienteId deve ser maior do que 0");

            RuleFor(x => x.Endereco)
                .NotEmpty()
                .WithMessage("Endereço não pode ser vazio")
                .MaximumLength(150)
                .WithMessage("Endereço deve ter no máximo 150 caracteres");
                        
            RuleFor(x => x.Estado)
                .NotEmpty()
                .WithMessage("Estado não pode ser vazio")
                .MaximumLength(2)
                .WithMessage("Estado deve ter no máximo 2 caracteres");

            RuleFor(x => x.Numero)
                .NotEmpty()
                .WithMessage("Número não pode ser vazio")
                .MaximumLength(20)
                .WithMessage("Número deve ter no máximo 20 caracteres");
        }
    }
}
