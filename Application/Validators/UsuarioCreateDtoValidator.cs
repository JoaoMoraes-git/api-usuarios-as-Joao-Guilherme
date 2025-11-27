using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// using Application.DTOs;
using FluentValidation;

namespace api_usuarios_as_Joao_Guilherme.Application.Validators
{
    public class UsuarioCreateValidator : AbstractValidator<UsuarioCreateDto>
    {
        public UsuarioCreateValidator()
        {
            RuleFor(p => p.Nome)
                .NotEmpty()
                .WithMessage("Nome do usuário é obrigatório")
                .MaximumLength(100)
                .WithMessage("Nome do usuário não pode passar de 100 caractéres")
                .MinimumLength(3)
                .WithMessage("Nome do usuário não pode ter menos que 3 caractéres");

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("Campo de email é obrigatório")
                .EmailAddress()
                .WithMessage("Formato inválido para o email");
                //Email unico

                //Resto aqui
        }

    
    }
}