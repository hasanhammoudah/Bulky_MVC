using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFormDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFormDb != null)
            {
                objFormDb.Title = obj.Title;
                objFormDb.ISBN = obj.ISBN;
                objFormDb.ListPrice = obj.ListPrice;
                objFormDb.Price = obj.Price;
                objFormDb.Price100 = obj.Price100;
                objFormDb.Price50 = obj.Price50;
                objFormDb.Author = obj.Author;
                objFormDb.Description = obj.Description;
                objFormDb.CategoryId = obj.CategoryId;
                if(obj.ImageUrl != null)
                {
                    objFormDb.ImageUrl = obj.ImageUrl;
                }
            }
         }

     
    }
}
