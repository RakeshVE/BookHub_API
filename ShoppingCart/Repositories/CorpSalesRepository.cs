using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.DTOs;
using ShoppingCart.Interfaces;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Repositories
{
    public class CorpSalesRepository:ICorpSalesRepository
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;

        public CorpSalesRepository(ShoppingCartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> AddCorpSalesAsync(CorpSalesDto corpsales)
        {
            var corp = new CorpSale()
            {
                CorpSalesId = corpsales.CorpSalesId,
                FirstName = corpsales.FirstName,
                LastName = corpsales.LastName,
                Phone=corpsales.Phone,
                Email=corpsales.Email,
                CompanyName=corpsales.CompanyName,
                State=corpsales.State,
                Country=corpsales.Country,
                Details=corpsales.Details,
                Purpose = corpsales.Purpose,
                CreatedBy=corpsales.CreatedBy,
                Createdn=corpsales.Createdn,
                ModifiedBy =corpsales.ModifiedBy,
                ModifiedOn=corpsales.ModifiedOn
            };
            _context.CorpSales.Add(corp);
            await _context.SaveChangesAsync();
            return corp.CorpSalesId;
        }

        public async Task DeleteCorpSalesAsync(int id)
        {
            var corpsales = new CorpSale() { CorpSalesId = id };
            _context.CorpSales.Remove(corpsales);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CorpSalesDto>> GetCorpSalesAsync()
        {
            var corpsales = await _context.CorpSales.ToListAsync();
            return _mapper.Map<List<CorpSalesDto>>(corpsales);
        }

        public async Task<CorpSalesDto> GetCorpSalesByIdAsync(int id)
        {
            var corpsales = await _context.CorpSales.FindAsync(id);
            return _mapper.Map<CorpSalesDto>(corpsales);
        }

        public async Task UpdateCorpSalesAsync(int id, CorpSalesDto corpsales)
        {
            var corp = await _context.CorpSales.FindAsync(id);
            if (corp!=null)
            {
                
                corp.FirstName = corpsales.FirstName;
                corp.LastName = corpsales.LastName;
                corp.Phone = corpsales.Phone;
                corp.Email = corpsales.Email;
                corp.CompanyName = corpsales.CompanyName;
                corp.State = corpsales.State;
                corp.Country = corpsales.Country;
                corp.Details = corpsales.Details;
                corp.Purpose = corpsales.Purpose;
                corp.CreatedBy = corpsales.CreatedBy;
                corp.Createdn = corpsales.Createdn;
                corp.ModifiedBy = corpsales.ModifiedBy;
                corp.ModifiedOn = corpsales.ModifiedOn;
                await _context.SaveChangesAsync();
            }
        }

        //public async Task UpdateCorpSalesPatchAsync(int id, JsonPatchDocument corpsales)
        //{
        //    var corp = await _context.CorpSales.FindAsync(id);
        //    if (corp!=null)
        //    {
        //        corpsales.ApplyTo(corp);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }
}
