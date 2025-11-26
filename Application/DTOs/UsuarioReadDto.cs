public record UsuarioReadDto(
    int Id,
    string Nome,
    string Email,
    string senha,
    DateTime DataNascimento,
    string? Telefone,
    bool Ativo,
    DateTime DataCriacao,
    DateTime DataAtualizacao
);