using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.domain;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Infrastructure.Persistence;

namespace api_usuarios_as_João_Guilherme.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        //Consulta todas as entidades
        public async Task<IEnumerable<Usuario>> GetAllAsync (CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Procura a entidade com o id enviado
        public async Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Procura a entidade com o email enviado
        public async Task<Usuario?> GetEmailAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Adiciona uma nova entidade ao banco de dados
        public async Task AddAsync(Usuario usuario, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Atualiza uma entidade do banco de dados
        public async Task UpdateAsync(Usuario usuario, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Remove uma entidade do banco de dados
        public async Task RemoveAsync(Usuario usuario, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Verifica se o email enviado já existe
        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Salva as alterações feitas
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}