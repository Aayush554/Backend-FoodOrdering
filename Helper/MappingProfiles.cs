using AutoMapper;
using FoodOrderingApi.Dto;
using FoodOrderingApi.Model;

/*
 * NAME
 * 
 * MappingProfiles - AutoMapper profile for mapping between DTOs and model entities.
 * 
 * DESCRIPTION
 * 
 * This class defines AutoMapper profiles for mapping between Data Transfer Objects (DTOs) and model entities.
 * It specifies how to map properties from one object type to another.
 */
namespace FoodOrderingApi.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // CreateMap for User to UserDto mapping
            CreateMap<User, UserDto>();

            // CreateMap for Order to OrderDto mapping
            CreateMap<Order, OrderDto>();

            // CreateMap for Review to ReviewDto mapping
            CreateMap<Review, ReviewDto>();

            // CreateMap for Payment to PaymentDto mapping
            CreateMap<Payment, PaymentDto>();

            // CreateMap for MenuItem to MenuItemDto mapping
            CreateMap<MenuItem, MenuItemDto>();

            // CreateMap for Category to CategoryDto mapping
            CreateMap<Category, CategoryDto>();

            // CreateMap for Cart to CartDto mapping
            CreateMap<Cart, CartDto>();

            // Reverse mapping for UserDto to User
            CreateMap<UserDto, User>();

            // Reverse mapping for OrderDto to Order
            CreateMap<OrderDto, Order>();

            // Reverse mapping for ReviewDto to Review
            CreateMap<ReviewDto, Review>();

            // Reverse mapping for PaymentDto to Payment
            CreateMap<PaymentDto, Payment>();

            // Reverse mapping for MenuItemDto to MenuItem
            CreateMap<MenuItemDto, MenuItem>();

            // Reverse mapping for CategoryDto to Category
            CreateMap<CategoryDto, Category>();

            // Reverse mapping for CartDto to Cart
            CreateMap<CartDto, Cart>();
        }
    }
}
