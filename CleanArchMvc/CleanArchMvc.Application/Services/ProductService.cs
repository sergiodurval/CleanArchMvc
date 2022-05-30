using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public async Task Add(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.CreateAsync(productEntity);
        }

        public async Task<ProductDTO> GetById(int? Id)
        {
            var productEntity = await _productRepository.GetByIdAsync(Id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<ProductDTO> GetProductCategory(int? Id)
        {
            var productEntity = await _productRepository.GetProductCategoryAsync(Id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productEntity = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
        }

        public async Task Remove(int? Id)
        {
            var productEntity = await _productRepository.GetByIdAsync(Id);
            await _productRepository.RemoveAsync(productEntity);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productEntity = _mapper.Map<Product>(productDTO);
            await _productRepository.UpdateAsync(productEntity);
        }
    }
}
