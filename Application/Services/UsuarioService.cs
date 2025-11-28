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
            throw new NotImplementedException();
        }

        public async Task<bool> EmailJaCadastradoAsync(string email, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        //Ficar de olho nesse método
        public async Task<IEnumerable<UsuarioReadDto>> ListarAsync(CancellationToken ct = default)
        {
            var usuarios = await _repo.GetAllAsync(ct);

            return usuarios.Select(u => new UsuarioReadDto(
                u.Id,
                u.Nome,
                u.Email,
                u.Senha,
                u.DataNascimento,
                u.Telefone,
                u.Ativo,
                u.DataCriacao,
                u.DataAtualizacao ?? default
            ));
        }

        public async Task<UsuarioReadDto?> ObterAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}