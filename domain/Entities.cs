using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_usuarios_as_João_Guilherme.domain
{
    public class Entities
    {
        public int Id { get; set; } //PK, Auto-increment - X
        public string Nome { get; set; } //Obrigatório, 3-100 caracteres - X
        public string Email { get; set; } //Obrigatório, formato válido, único - X
        public string Senha { get; set; } //Obrigatório, min 6 caracteres - X
        public DateTime DataNascimento { get; set; } //Obrigatório, idade >= 18 anos - X
        public string Telefone { get; set; } //Opcional, formato (XX) XXXXX-XXXX (talvez usar string?)
        public bool Ativo { get; set; } //Obrigatório, default true
        public DateTime DataCriacao { get; set; } //Obrigatório, preenchido automaticamente
        public DateTime? DataAtualizacao { get; set; } // Opcional, atualizacao automaticamente

    }
}