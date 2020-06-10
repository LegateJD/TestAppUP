using TestApp.WEB.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.WEB.Infrastructure.Managers
{
    public class CookieBasketManager : IBasketManager
    {
        private const string CookieBasketName = "basket";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductService _productService;

        public CookieBasketManager(IHttpContextAccessor httpContextAccessor, IProductService productService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productService = productService;
        }

        public Order GetBasket()
        {
            var viewModel = new Order
            {
                OrderDetails = new List<OrderDetails>(),
            };

            var serializedOrders = _httpContextAccessor.HttpContext.Request.Cookies[CookieBasketName];

            if (!string.IsNullOrEmpty(serializedOrders))
            {
                var orderDictionary = (Dictionary<Guid, short>)Utils.StringToObject(serializedOrders);
                var ids = orderDictionary.Keys.ToList();
                var products = ids.Select(c => _productService.GetProductById(c));

                foreach (var item in products)
                {
                    var orderDetails = new OrderDetails
                    {
                        Quantity = orderDictionary[item.Id],
                        Product = item,
                    };

                    viewModel.OrderDetails.Add(orderDetails);
                }
            }

            return viewModel;
        }

        public void AddOrderDetails(Guid productId, short quantity)
        {
            if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieBasketName))
            {
                var orderDictionary = new Dictionary<Guid, short>
                {
                    { productId, quantity },
                };

                var base64Orders = Utils.ObjectToString(orderDictionary);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieBasketName, base64Orders);
            }
            else
            {
                var serializedOrders = _httpContextAccessor.HttpContext.Request.Cookies[CookieBasketName];
                var orderDictionary = (Dictionary<Guid, short>)Utils.StringToObject(serializedOrders);

                if (orderDictionary.ContainsKey(productId))
                {
                    orderDictionary[productId] += quantity;
                }
                else
                {
                    orderDictionary.Add(productId, quantity);
                }

                var base64Orders = Utils.ObjectToString(orderDictionary);
                _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieBasketName, base64Orders);
            }
        }

        public void RemoveOrderDetails(Guid productId)
        {
            if (!_httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(CookieBasketName))
            {
                return;
            }

            var serializedOrders = _httpContextAccessor.HttpContext.Request.Cookies[CookieBasketName];
            var orderDictionary = (Dictionary<Guid, short>)Utils.StringToObject(serializedOrders);
            orderDictionary.Remove(productId);
            var base64Orders = Utils.ObjectToString(orderDictionary);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookieBasketName, base64Orders);
        }

        public void ClearBasket()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(CookieBasketName);
        }
    }
}
