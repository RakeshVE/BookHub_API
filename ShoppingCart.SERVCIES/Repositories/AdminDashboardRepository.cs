using ShoppingCart.Interfaces;
using ShoppingCart.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.DTO.DTOs;

namespace ShoppingCart.Repositories
{
    public class AdminDashboardRepository: IAdminDashboardRepository
    {
        private readonly ShoppingCartContext _context;

        public AdminDashboardRepository(ShoppingCartContext context)
        {
            _context = context;
        }
        public async Task<AdminDashBoardDto> DashboardData() {
            
            try
            {
                AdminDashBoardDto obj = new AdminDashBoardDto();
                var NoOfUser = _context.Users.Count();
                obj.TotalUsers = NoOfUser;
                var NoOfUserActive =  _context.Users.Where(x => x.IsActive == true).Count();
                obj.TotalActiveUsers = NoOfUserActive;
                decimal TotalSales = (decimal)_context.OrderDetails.Sum(x => x.Price);
                obj.TotalSales = TotalSales;
                var TotalItemSales = _context.OrderDetails.Count();
                obj.TotalItemSale = TotalItemSales;
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
