using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulkyBook.Data.IRepository;
using BulkyBook.Models;

namespace BulkyBook.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var updatedProduct = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (updatedProduct != null)
            {
                updatedProduct.Name = product.Name;
                updatedProduct.Description = product.Description;
                updatedProduct.ISBN = product.ISBN;
                updatedProduct.ListPrice = product.ListPrice;
                updatedProduct.Price = product.Price;
                updatedProduct.Price50 = product.Price50;
                updatedProduct.Price100 = product.Price100;
                updatedProduct.Author = product.Author;
                updatedProduct.CategoryId = product.CategoryId;
                updatedProduct.CoverTypeId = product.CoverTypeId;
                if (updatedProduct.ImageUrl != null)
                {
                    updatedProduct.ImageUrl = updatedProduct.ImageUrl;
                }
            }
        }
    }
}
