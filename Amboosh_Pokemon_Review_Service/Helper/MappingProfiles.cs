using Amboosh_Pokemon_Review_Service.Dto;
using Amboosh_Pokemon_Review_Service.Model;
using AutoMapper;

namespace Amboosh_Pokemon_Review_Service.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDto>();
        CreateMap<Category, CategoryDto>();
    }
}