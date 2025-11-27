public record UsuarioUpdateDto(
    string Nome,
    string Email,
    string Senha,
    DateTime DataNascimento,
    string? Telefone,
    bool Ativo,
    DateTime DataAtualizacao
);