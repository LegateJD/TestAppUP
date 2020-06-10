using AutoMapper;
using TestApp.WEB.Models.Customers;
using TestApp.WEB.Models.Orders;
using TestApp.WEB.Models.Product;
using System;
using TestApp.Domain.Models;

namespace TestApp.WEB.Configuration
{
    public class WebMapperProfile : Profile
    {
        public WebMapperProfile()
        {
            CreateMap<EditProductViewModel, Product>();
            CreateMap<Product, EditProductViewModel>();
            CreateMap<CreateProductViewModel, Product>();
            CreateMap<Product, ShowProductViewModel>()
                .ReverseMap();
            CreateMap<Customer, CustomerViewModel>()
                .ReverseMap();
            CreateMap<CreateOrderDetailsViewModel, OrderDetails>()
                .ForMember(c => c.Product, opt => opt.MapFrom(c => new Product { Id = c.ProductId }));
            CreateMap<Order, OrderViewModel>()
                .ReverseMap();
            CreateMap<OrderDetails, OrderDetailsViewModel>()
                .ReverseMap();
        }
    }
}
