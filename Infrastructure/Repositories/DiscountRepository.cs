using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly DiscountDbContext _context;

        public DiscountRepository(IConfiguration configuration,DiscountDbContext context)
        {
            _configuration = configuration;
            this._context = context;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                    ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return new Coupon { Productname = "No Discount", Amount = 0, Description = "No Discount Availables" };
            }
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {

            try
            {
                _context.Coupons.Add(coupon);
                var data = await _context.SaveChangesAsync();
                return data > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            //var affected = await connection.ExecuteAsync
            //    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            //    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            //if (affected == 0)
            //{
            //    return false;
            //}
            //return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new { ProductName = coupon.Productname, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }


    }
}