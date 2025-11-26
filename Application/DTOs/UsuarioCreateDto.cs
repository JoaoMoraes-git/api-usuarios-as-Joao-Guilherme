public record UsuarioCreateDto(
    string Nome,
    string Email,
    string senha,
    DateTime DataNascimento,
    string? Telefone
);
