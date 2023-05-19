using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Model;

namespace FoodOrderingApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<UserDto, User>();
            CreateMap<OrderDto, Order>();
            CreateMap<ReviewDto, Review>();
            CreateMap<PaymentDto, Payment>();
            CreateMap<MenuItemDto, MenuItem>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CartDto, Cart>();

        }
    }
}
