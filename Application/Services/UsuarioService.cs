using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Application.DTOs;
using api_usuarios_as_João_Guilherme.Infrastructure.Repositories;
using AutoMapper;
using api_usuarios_as_João_Guilherme.domain;

namespace api_usuarios_as_João_Guilherme.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }


        public async Task<UsuarioReadDto> AtualizarAsync(int id, UsuarioUpdateDto dto, CancellationToken ct = default)
        {
            var usuarioEncontrado = await _repo.GetByIdAsync(id, ct);
            if (usuarioEncontrado == null) return null;

            _mapper.Map(dto, usuarioEncontrado);

            await _repo.UpdateAsync(usuarioEncontrado, ct);
            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<UsuarioReadDto>(usuarioEncontrado);
        }

        public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct = default)
        {
            if (dto == null)
                return null;

            if (string.IsNullOrWhiteSpace(dto.Nome))
                return null;

            if (string.IsNullOrWhiteSpace(dto.Email))
                return null;

            if (string.IsNullOrWhiteSpace(dto.Senha))
                return null;

            if (dto.DataNascimento == default)
                return null;

            var usuario = _mapper.Map<Usuario>(dto);
            
            //Campo de telefone é opcional
            if (!string.IsNullOrWhiteSpace(dto.Telefone))
            {
                usuario.Telefone = dto.Telefone.Trim();
            }

            //Campos definidos pelo sistema
            usuario.DataCriacao = DateTime.UtcNow;
            usuario.Ativo = true;

            await _repo.AddAsync(usuario, ct);
            await _repo.SaveChangesAsync(ct);

            return _mapper.Map<UsuarioReadDto>(usuario);

        }

        public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _repo.EmailExistsAsync(email, ct);
        }

        public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct = default)
        {
            var usuarios = await _repo.GetAllAsync(ct);

            return usuarios.Select(u => new UsuarioReadDto(
                u.Id,
                u.Nome,
                u.Email,
                u.DataNascimento,
                u.Telefone,
                u.Ativo,
                u.DataCriacao,
                u.DataAtualizacao
            ));
        }

        public async Task<UsuarioReadDto?> ObterAsync(int id, CancellationToken ct = default)
        {
            if (id <= 0)
                return null;

            var usuario = await _repo.GetByIdAsync(id, ct);
            if (usuario == null)
                return null;

            return _mapper.Map<UsuarioReadDto>(usuario);
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken ct = default)
        {
            var usuario = await _repo.GetByIdAsync(id, ct);
            if(usuario == null) return false;
            
            usuario.Ativo = false;
            usuario.DataAtualizacao = DateTime.UtcNow;
            
            await _repo.UpdateAsync(usuario, ct);
            await _repo.SaveChangesAsync(ct);
            return true;
        }
    }
}