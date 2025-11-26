public record UsuarioUpdateDto(
    string Nome,
    string Email,
    string senha,
    DateTime DataNascimento,
    string? Telefone,
    bool Ativo,
    DateTime DataAtualizacao
);