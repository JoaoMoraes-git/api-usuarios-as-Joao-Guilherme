using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.Application.DTOs;

namespace api_usuarios_as_João_Guilherme.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct = default);
        Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct = default);
        Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct = default);
        Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct = default);
        Task<bool> RemoverAsync (int id, CancellationToken ct = default);
        Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default);
    }
}