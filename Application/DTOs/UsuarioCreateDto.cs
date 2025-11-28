using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_usuarios_as_João_Guilherme.Application.DTOs
{
    public record UsuarioCreateDto(
    
        string Nome,
        string Email,
        string Senha,
        DateTime DataNascimento,
        string? Telefone
    );
    
}

