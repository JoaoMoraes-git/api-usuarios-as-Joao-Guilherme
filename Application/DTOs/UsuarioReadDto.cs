using System.Security.Cryptography.X509Certificates;

namespace api_usuarios_as_João_Guilherme.Application.DTOs
{
    public record UsuarioReadDto(
        int Id,
        string Nome,
        string Email,
        DateTime DataNascimento,
        string? Telefone,
        bool Ativo,
        DateTime DataCriacao,
        DateTime? DataAtualizacao
    );
}