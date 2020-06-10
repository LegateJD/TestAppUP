using TestApp.Domain.Models;
using System;
using System.Collections.Generic;

namespace TestApp.Domain.Interfaces
{
    public interface IProductService
    {
        void CreateProduct(Product product);

        void UpdateProduct(Product product);

        Product GetProductById(Guid productId);

        IEnumerable<Product> GetAllProducts();

        int GetCountOfProducts();

        void DeleteProduct(Guid product);
    }
}
