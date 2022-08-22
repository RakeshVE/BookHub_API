using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.DTO.DTOs;
using ShoppingCart.Interfaces;

namespace ShoppingCart.BLL.Class
{
   public class AdminDashboardBL
    {
        private IAdminDashboardRepository _AdminDashboardRepository;

        public AdminDashboardBL(IAdminDashboardRepository adminDashboardRepository)
        {
            _AdminDashboardRepository = adminDashboardRepository;
        }
        public async Task<AdminDashBoardDto> DashboardData() {
           return await _AdminDashboardRepository.DashboardData();
        }
    }
}
