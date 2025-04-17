using AutoMapper;
using EcommerceAPI.Data.CDDtos;
using EcommerceAPI.Models;

namespace EcommerceAPI.Profiles
{
    public class CDProfiles : Profile
    {
        public CDProfiles()
        { //CONVERTER       DE       PARA
            CreateMap<CreateCDDto, CDModel>();
            CreateMap<UpdateCDDto, CDModel>();
            CreateMap<CDModel, ReadCDDto>();
        }
    }
}
