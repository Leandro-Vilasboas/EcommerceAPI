using AutoMapper;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Models;

namespace EcommerceAPI.Profiles
{
    public class SubcategoriaProfiles : Profile
    {
        public SubcategoriaProfiles()
        { //CONVERTER       DE             ,       PARA
            CreateMap<CreateSubcategoriaDto, SubcategoriaModel>();
            CreateMap<UpdateSubcategoriaDto, SubcategoriaModel>();
            CreateMap<SubcategoriaModel, ReadSubcategoriaDto>();
        }
    }
}
