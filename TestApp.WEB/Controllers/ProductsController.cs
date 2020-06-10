using AutoMapper;
using TestApp.WEB.Models.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestApp.Domain.Interfaces;
using TestApp.Domain.Models;

namespace TestApp.WEB.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [Route("products/new", Name = "defaultCreateProductGet")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateProductViewModel());
        }

        [Route("products/new", Name = "defaultCreateProductPost")]
        [HttpPost]
        public IActionResult Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(model);
                _productService.CreateProduct(product);

                return RedirectToRoute("getAllProductsEmpty");
            }

            return View(model);
        }

        [Route("product/update/{productId}", Name = "defaultEditProductGet")]
        [HttpGet]
        public IActionResult Edit(Guid productId)
        {
            var product = _productService.GetProductById(productId);
            var viewModel = _mapper.Map<EditProductViewModel>(product);

            return View(viewModel);
        }

        [Route("products/update", Name = "defaultEditProductPost")]
        [HttpPost]
        public IActionResult Edit([FromForm] EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editedProduct = _mapper.Map<Product>(model);
                _productService.UpdateProduct(editedProduct);
                return RedirectToRoute("getAllProductsEmpty");
            }

            return View(model);
        }

        [Route("products/remove", Name = "defaultRemoveProductPost")]
        [HttpPost]
        public IActionResult Remove([FromForm] DeleteProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _productService.DeleteProduct(viewModel.ProductId);
            }

            return RedirectToRoute("getAllProductsEmpty");
        }

        [Route("product/{productId}", Name = "defaultGetProduct")]
        [HttpGet]
        public IActionResult Get(Guid productId)
        {
            var product = _mapper.Map<ShowProductViewModel>(_productService.GetProductById(productId));

            return View(product);
        }

        [Route("products", Name = "getAllProduct")]
        [Route("", Name = "getAllProductsEmpty")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _mapper.Map<List<ShowProductViewModel>>(_productService.GetAllProducts());

            var viewModel = new ShowProductsViewModel
            {
                Products = products,
            };

            return View(viewModel);
        }
    }
}
