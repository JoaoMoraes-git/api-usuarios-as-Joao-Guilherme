using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_usuarios_as_João_Guilherme.Application.Interfaces;
using api_usuarios_as_João_Guilherme.Application.DTOs;
using api_usuarios_as_João_Guilherme.Infrastructure.Repositories;
using AutoMapper;
using api_usuarios_as_João_Guilherme.domain;
using System.Linq.Expressions;

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
            if (usuarioEncontrado == null)
            {
                return null;
            }

            _mapper.Map(dto, usuarioEncontrado);

            usuarioEncontrado.Email = usuarioEncontrado.Email.Trim().ToLower();
            usuarioEncontrado.DataAtualizacao = DateTime.UtcNow;

            await _repo.UpdateAsync(usuarioEncontrado, ct);
            await _repo.SaveChangesAsync(ct);
            return _mapper.Map<UsuarioReadDto>(usuarioEncontrado);
        }

        public async Task<UsuarioReadDto> CriarAsync(UsuarioCreateDto dto, CancellationToken ct = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Nome)) throw new ArgumentException("Nome é obrigatório", nameof(dto.Nome));
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email é obrigatório", nameof(dto.Email));
            if (string.IsNullOrWhiteSpace(dto.Senha)) throw new ArgumentException("Senha é obrigatória", nameof(dto.Senha));
            if (dto.DataNascimento == default) throw new ArgumentException("Data de nascimento é obrigatória", nameof(dto.DataNascimento));

            // Normalizar email
            var emailUsado = dto.Email.Trim().ToLowerInvariant();

            // Verificar idade >= 18
            var hoje = DateTime.UtcNow.Date;
            var idade = hoje.Year - dto.DataNascimento.Date.Year;
            if (dto.DataNascimento.Date > hoje.AddYears(-idade)) idade--;
            if (idade < 18) throw new ArgumentException("Usuário deve ter pelo menos 18 anos");

            // Checar email já cadastrado
            if (await _repo.EmailExistsAsync(emailUsado, null, ct))
                throw new InvalidOperationException("Email já cadastrado");

            // Mapear DTO para entidade e ajustar campos
            var usuario = _mapper.Map<Usuario>(dto);
            usuario.Email = emailUsado;
            if (!string.IsNullOrWhiteSpace(dto.Telefone))
                usuario.Telefone = dto.Telefone.Trim();

            usuario.DataCriacao = DateTime.UtcNow;
            usuario.DataAtualizacao = null;
            usuario.Ativo = true;

            await _repo.AddAsync(usuario, ct);

            try
            {
                await _repo.SaveChangesAsync(ct);
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                // Proteção contra race condition / constraint no DB
                throw new InvalidOperationException("Falha ao salvar: email já cadastrado (constraint)", ex);
            }

            return _mapper.Map<UsuarioReadDto>(usuario);
        }

        public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            return await _repo.EmailExistsAsync(email, ct: ct);
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