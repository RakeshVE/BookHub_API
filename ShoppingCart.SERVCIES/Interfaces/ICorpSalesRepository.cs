using Microsoft.AspNetCore.JsonPatch;
using ShoppingCart.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface ICorpSalesRepository
    {
        Task<List<CorpSalesDto>> GetCorpSalesAsync();
        Task<CorpSalesDto> GetCorpSalesByIdAsync(int id);
        Task<int> AddCorpSalesAsync(CorpSalesDto corpsales);
        Task UpdateCorpSalesAsync(int id, CorpSalesDto corpsales);
        //Task UpdateCorpSalesPatchAsync(int id, JsonPatchDocument corpsales);
        Task DeleteCorpSalesAsync(int id);
    }
}
