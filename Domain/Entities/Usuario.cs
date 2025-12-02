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
        public int Id { get; set; } //PK, Auto-increment - V

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; } = string.Empty; //Obrigatório, 3-100 caracteres - V

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; //Obrigatório, formato válido, único - V

        [Required]
        [MinLength(6)]
        public string Senha { get; set; } = string.Empty; //Obrigatório, min 6 caracteres - V

        [Required]
        public DateTime DataNascimento { get; set; } //Obrigatório, idade >= 18 anos - V

        public string? Telefone { get; set; } //Opcional, formato (XX) XXXXX-XXXX - V

        [Required]
        public bool Ativo { get; set; } = true; //Obrigatório, default true - V

        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now; //Obrigatório, preenchido automaticamente - V

        public DateTime? DataAtualizacao { get; set; } // Opcional, atualizacao automaticamente - V

    }
}