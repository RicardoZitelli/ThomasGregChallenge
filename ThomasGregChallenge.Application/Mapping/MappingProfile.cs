﻿using AutoMapper;
using ThomasGregChallenge.Application.DTOs.Requests;
using ThomasGregChallenge.Application.DTOs.Responses;
using ThomasGregChallenge.Domain.Entities;

namespace ThomasGregChallenge.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<ClienteRequestDto, Cliente>();                
            CreateMap<Cliente, ClienteResponseDto>();

            CreateMap<LogradouroRequestDto, Logradouro>();
            CreateMap<Logradouro, LogradouroResponseDto>();                      
        }        
    }
}
