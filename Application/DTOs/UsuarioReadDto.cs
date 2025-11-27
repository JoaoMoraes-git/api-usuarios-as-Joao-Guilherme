using System.Security.Cryptography.X509Certificates;

public record UsuarioReadDto(
    int Id,
    string Nome,
    string Email,
    string Senha,
    DateTime DataNascimento,
    string? Telefone,
    bool Ativo,
    DateTime DataCriacao,
    DateTime? DataAtualizacao
);