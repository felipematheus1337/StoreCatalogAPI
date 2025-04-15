using AutoMapper;
using StoreCatalogAPI.Models;

namespace StoreCatalogAPI.DTOs.Mappings
{
    public class DTOMappingProfile : Profile
    {

        public DTOMappingProfile() {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
        

    }
}
