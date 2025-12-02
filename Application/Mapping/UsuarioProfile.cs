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
            //Mapeamento de leitura
            CreateMap<Usuario, UsuarioReadDto>();

            //Mapeamento de criação
            CreateMap<UsuarioCreateDto, Usuario>();

            //Mapeamento de atualização
            CreateMap<UsuarioUpdateDto, Usuario>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())

                .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())

                .ForMember(dest => dest.Senha, opt => opt.Ignore());
        }
        

    }
}