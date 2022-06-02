using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator )
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task<ProductDTO> GetById(int? Id)
        {
            var getProductById = new GetProductByIdQuery(Id.Value);

            if (getProductById == null)
                throw new Exception("Entity could not be loaded");

            var result = await _mediator.Send(getProductById);
            return _mapper.Map<ProductDTO>(result);
        }

        //public async Task<ProductDTO> GetProductCategory(int? Id)
        //{
        //    var getProductById = new GetProductByIdQuery(Id.Value);

        //    if (getProductById == null)
        //        throw new Exception("Entity could not be loaded");

        //    var result = await _mediator.Send(getProductById);
        //    return _mapper.Map<ProductDTO>(result);
        //}

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            var productQuery = new GetProductsQuery();

            if (productQuery == null)
                throw new Exception("Entity could not be loaded");

            var result = await _mediator.Send(productQuery);

            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task Remove(int? Id)
        {
            var productRemoveCommand = new ProductRemoveCommand(Id.Value);
            
            if (productRemoveCommand == null)
                throw new Exception("Entity could not be loaded");

            await _mediator.Send(productRemoveCommand);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }
    }
}
