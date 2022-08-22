using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.BLL.Class
{
  public  class CorpSalesBL
    {
        private readonly ICorpSalesRepository _corpsales;

        public CorpSalesBL(ICorpSalesRepository corpsales)
        {
            _corpsales = corpsales;
        }

        public async Task<int> AddCorpSalesAsync(CorpSalesDto corpsales)
        {
            return await _corpsales.AddCorpSalesAsync(corpsales);
        }

        public async Task DeleteCorpSalesAsync(int id)
        {
             await _corpsales.DeleteCorpSalesAsync(id);

        }

        public async Task<List<CorpSalesDto>> GetCorpSalesAsync()
        {
            return await _corpsales.GetCorpSalesAsync();

        }

        public async Task<CorpSalesDto> GetCorpSalesByIdAsync(int id)
        {
            return await _corpsales.GetCorpSalesByIdAsync(id);

        }

        public async Task UpdateCorpSalesAsync(int id, CorpSalesDto corpsales)
        {
             await _corpsales.UpdateCorpSalesAsync(id,corpsales);

        }
    }
}
