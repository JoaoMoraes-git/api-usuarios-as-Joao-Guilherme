using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using api_usuarios_as_João_Guilherme.Application.DTOs;
using api_usuarios_as_João_Guilherme.domain;

namespace api_usuarios_as_João_Guilherme.Application.Mapping
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioReadDto>();

            CreateMap<UsuarioCreateDto, Usuario>();

            CreateMap<UsuarioUpdateDto, Usuario>();
        }
        

    }
}