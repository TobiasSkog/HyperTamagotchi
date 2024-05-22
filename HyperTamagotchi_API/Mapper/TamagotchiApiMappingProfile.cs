using AutoMapper;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;

namespace HyperTamagotchi_API.Mapper;

public class TamagotchiApiMappingProfile : Profile
{
    public TamagotchiApiMappingProfile()
    {
        // DTO to Model mapping
        CreateMap<ShoppingCart, ShoppingCartDto>();
        CreateMap<ShoppingItem, ShoppingItemDto>();
        CreateMap<Tamagotchi, TamagotchiDto>();
        CreateMap<Order, OrderDto>();

        // Model to DTO mapping
        CreateMap<ShoppingCartDto, ShoppingCart>();
        CreateMap<ShoppingItemDto, ShoppingItem>();
        CreateMap<OrderDto, Order>();
        //CreateMap<ShoppingCartDto, ShoppingCart>();

    }
}
