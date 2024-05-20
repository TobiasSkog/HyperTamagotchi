using AutoMapper;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;

namespace HyperTamagotchi_API.Mapper;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>();
    }
}
