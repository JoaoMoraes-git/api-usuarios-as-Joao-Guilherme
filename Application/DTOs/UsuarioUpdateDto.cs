
namespace api_usuarios_as_João_Guilherme.Application.DTOs
{
    public record UsuarioUpdateDto(
    string Nome,
    string Email,
    DateTime DataNascimento,
    string? Telefone,
    bool Ativo
);
}
