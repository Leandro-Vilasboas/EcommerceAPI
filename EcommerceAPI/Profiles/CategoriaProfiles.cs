using AutoMapper;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Models;

namespace EcommerceAPI.Profiles
{
    public class CategoriaProfiles : Profile
    {
        public CategoriaProfiles()
        { //CONVERTER         DE        ,       PARA
            CreateMap<CreateCategoriaDto, CategoriaModel>();
            CreateMap<UpdateCategoriaDto, CategoriaModel>();
            CreateMap<CategoriaModel, ReadCategoriaDto>();
        }
    }
}
