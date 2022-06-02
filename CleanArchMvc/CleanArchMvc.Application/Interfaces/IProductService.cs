using CleanArchMvc.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetById(int? Id);
        //Task<ProductDTO> GetProductCategory(int? Id);
        Task Add(ProductDTO productDTO);
        Task Update(ProductDTO productDTO);
        Task Remove(int? Id);
    }
}
