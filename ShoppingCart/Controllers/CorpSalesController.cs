using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorpSalesController : ControllerBase
    {
        private readonly ICorpSalesRepository _corpsales;
        public CorpSalesController(ICorpSalesRepository corpsales)
        {
            _corpsales = corpsales;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetCorpSales()
        {
            var allcorpsales = await _corpsales.GetCorpSalesAsync();
            return Ok(allcorpsales);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddCorpSales([FromBody] CorpSalesDto corp)
        {
            var corpsales = await _corpsales.AddCorpSalesAsync(corp);
            return CreatedAtAction(nameof(GetCorpSalesById), new { id = corpsales, controller = "CorpSales" }, corp); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCorpSalesById([FromRoute]int id)
        {
            var corpsales = await _corpsales.GetCorpSalesByIdAsync(id);
            if (corpsales==null)
            {
                return NotFound();
            }
            return Ok(corpsales);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCorpSales([FromBody] CorpSalesDto corpsales, [FromRoute] int id)
        {
            await _corpsales.UpdateCorpSalesAsync(id, corpsales);
            return Ok();
        }
        //[HttpPatch("{id}")]
        //public async Task<IActionResult> UpdateCorpSalesPatch([FromBody] JsonPatchDocument corpsales, [FromRoute] int id)
        //{
        //    await _corpsales.UpdateCorpSalesPatchAsync(id, corpsales);
        //    return Ok();
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorpSales([FromRoute] int id)
        {
            await _corpsales.DeleteCorpSalesAsync(id);
            return Ok();
        }

    }
}
