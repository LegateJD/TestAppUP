using AutoMapper;
using TestApp.Domain.Interfaces.Repositories;
using TestApp.Domain.Interfaces.UnitsOfWork;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace TestApp.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Product> _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductService> logger, IRepository<Product> productRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _productRepository = productRepository;
        }

        public void CreateProduct(Product product)
        {
            product.PublishedDate = DateTime.UtcNow;
            product.Price = Math.Round(product.Price, 2);
            _productRepository.Insert(product);
            _unitOfWork.Commit();
            _logger.LogInformation($"Created new product with name \"{product.Name}\"");
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
            _unitOfWork.Commit();
            _logger.LogInformation($"Updated product with name \"{product.Name}\"");
        }

        public Product GetProductById(Guid productId)
        {
            return _productRepository.GetSingle(c => c.Id == productId);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _mapper.Map<List<Product>>(_productRepository.GetMany());
        }

        public int GetCountOfProducts()
        {
            return _productRepository.Count();
        }

        public void DeleteProduct(Guid productId)
        {
            _productRepository.Delete(productId);
            _unitOfWork.Commit();
            _logger.LogInformation($"Deleted product {productId}");
        }
    }
}
