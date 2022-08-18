using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private IAdminDashboardRepository _AdminDashboardRepository;

        public AdminDashboardController(IAdminDashboardRepository adminDashboardRepository)
        {
            _AdminDashboardRepository = adminDashboardRepository;
        }
        [HttpGet("GetDashBoardData")]
        public async Task<ActionResult> GetDashBoardData()
        {
            try
            {
            var obj =    await _AdminDashboardRepository.DashboardData();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                  return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
   
}
