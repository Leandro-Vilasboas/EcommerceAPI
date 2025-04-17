using AutoMapper;
using EcommerceAPI.Data.ProdutoDtos;
using EcommerceAPI.Models;

namespace EcommerceAPI.Profiles
{
    public class ProdutosProfiles : Profile
    {
        public ProdutosProfiles()
        { //CONVERTER       DE             ,       PARA
            CreateMap<CreateProdutoDto, ProdutoModel>();
            CreateMap<UpdateProdutoDto, ProdutoModel>();
            CreateMap<ProdutoModel, ReadProdutoDto>();
        }
    }
}
