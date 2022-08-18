using ShoppingCart.DTO.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Interfaces
{
    public interface IAdminDashboardRepository
    {
         Task<AdminDashBoardDto> DashboardData();
    }
}
