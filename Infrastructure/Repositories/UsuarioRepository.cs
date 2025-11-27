using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.domain;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace api_usuarios_as_João_Guilherme.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        //Consulta todas as entidades
        public async Task<IEnumerable<Usuario>> GetAllAsync (CancellationToken ct = default)
        {
            return await _context.Usuarios.AsNoTracking().ToListAsync(ct);
        }

        //Procura a entidade com o id enviado
        public async Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
        }

        //Procura a entidade com o email enviado
        public async Task<Usuario?> GetEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        //Adiciona uma nova entidade ao banco de dados
        public async Task AddAsync(Usuario usuario, CancellationToken ct = default)
        {
            await _context.Usuarios.AddAsync(usuario, ct);
            // await _context.SaveChangesAsync(ct);
        }

        //Atualiza uma entidade do banco de dados
        public Task UpdateAsync(Usuario usuario, CancellationToken ct = default)
        {
            _context.Usuarios.Update(usuario);
            return Task.CompletedTask;
        }

        //Remove uma entidade do banco de dados
        public async Task RemoveAsync(Usuario usuario, CancellationToken ct = default)
        {
            _context.Remove(usuario);
            // await _context.SaveChangesAsync(ct);
        }

        //Verifica se o email enviado já existe
        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email, ct = default);
        }

        //Salva as alterações feitas
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _context.SaveChangesAsync(ct);
        }
    }
}