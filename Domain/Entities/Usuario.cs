using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace api_usuarios_as_João_Guilherme.domain
{
    public class Usuario
    {
        [Key]
        //Auto-increment aqui
        public int Id { get; set; } //PK, Auto-increment - ...

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty; //Obrigatório, 3-100 caracteres - V

        [Required]
        [EmailAddress]
        // Único / FluentAPI aqui
        public string Email { get; set; } = string.Empty; //Obrigatório, formato válido, único - ... [FluentAPI]

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty; //Obrigatório, min 6 caracteres - V

        [Required]
        // Verificação de idade / FluentAPI aqui
        public DateTime DataNascimento { get; set; } //Obrigatório, idade >= 18 anos - ... [FluentAPI]

        // Verificação de formato / FluentAPI aqui
        public string? Telefone { get; set; } //Opcional, formato (XX) XXXXX-XXXX - X [FluentAPI]

        [Required]
        // Default / FluentAPI aqui
        public bool Ativo { get; set; } //Obrigatório, default true - ... [FluentAPI]

        [Required]
        // Preenchimento automatico / FluentAPI aqui
        public DateTime DataCriacao { get; set; } = DateTime.Now; //Obrigatório, preenchido automaticamente - X [FluentAPI]

        // Atualização automatica / FluentAPI aqui
        public DateTime? DataAtualizacao { get; set; } // Opcional, atualizacao automaticamente - x [FluentAPI]

    }
}