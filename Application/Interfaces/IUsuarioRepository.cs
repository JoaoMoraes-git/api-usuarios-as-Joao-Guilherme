using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.domain;

namespace api_usuarios_as_João_Guilherme.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync(CancellationToken ct = default);
        Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<Usuario?> GetEmailAsync(string email, CancellationToken ct = default);
        Task AddAsync(Usuario usuario, CancellationToken ct = default);
        Task UpdateAsync(Usuario usuario, CancellationToken ct = default);
        Task RemoveAsync(Usuario usuario, CancellationToken ct = default);
        Task<bool> EmailExistsAsync(string email, int? ignoreId = null, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}