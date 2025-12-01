using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Application.DTOs;
using api_usuarios_as_João_Guilherme.Infrastructure.Repositories;
using FluentValidation;
using System.Data;

namespace api_usuarios_as_Joao_Guilherme.Application.Validators
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateValidator(IUsuarioRepository repo)
        {
            RuleFor(u => u.Nome)
                .NotEmpty()
                .WithMessage("Nome do usuário é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome do usuário não pode passar de 100 caractéres")
                .MinimumLength(3)
                .WithMessage("Nome do usuário não pode ter menos que 3 caractéres");

            RuleFor(u => u.Email)
                .NotEmpty()
                .WithMessage("Campo de email é obrigatório")
                .EmailAddress()
                .WithMessage("Email com formato inváido")
                .MustAsync(async (email, ct) =>
                {
                    return !await repo.EmailExistsAsync(email, ct);
                })
                .WithMessage("Email já cadastrado");
          
            RuleFor (u => u.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatória")
                .MinimumLength(3)
                .WithMessage("Senha não pode ter menos que 3 caractéres");

            RuleFor (u => u.DataNascimento)
                .NotEmpty()
                .WithMessage("Data de nascimento é obrigatória")
                .Must (data =>
                {
                    var dataAtual = DateTime.Today;
                    var idade = dataAtual.Year - data.Year;

                    if (data.Date > dataAtual.AddYears(-idade))
                        idade--;

                    return idade >= 18;
                })
                .WithMessage("Usuário precisa ter pelo menos 18 anos");

            RuleFor (u => u.Telefone)
                .Matches(@"^\(\d{2}\) \d{5}-\d{4}$")
                .When(u => !string.IsNullOrWhiteSpace(u.Telefone))
                .WithMessage("Telefone deve estar no formato (XX) XXXXX-XXXX");
            
            
        }

    
    }
}