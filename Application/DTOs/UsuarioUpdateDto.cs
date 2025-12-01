
namespace api_usuarios_as_João_Guilherme.Application.DTOs
{
    public record UsuarioUpdateDto(
    // int Id, //Usuário não vai poder atualizar o id, campo adicionado para a atualização de email, 
    string Nome,
    string Email,
    DateTime DataNascimento,
    string? Telefone,
    bool Ativo
);
}
