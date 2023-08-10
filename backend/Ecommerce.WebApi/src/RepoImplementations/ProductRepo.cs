using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.src.Entities;
using Ecommerce.Domain.src.RepoInterfaces;
using Ecommerce.Domain.src.Shared;
using Ecommerce.WebApi.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.WebApi.src.RepoImplementations
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(DatabaseContext databaseContext) : base(databaseContext)
        {
        }


        // public new IEnumerable<Product> GetAll(QueryOptions queryOptions)
        // {
        //     return _products;
        // }
        // public Product CreateOne(Product product)
        // {
        //     _products.Add(product);
        //     _context.SaveChanges();
        //     return product;
        // }
        // public Product GetOneById(Guid id)
        // {
        //     return _products.Find(id);
        // }
        // public Product UpdateOne(Product updateProduct)
        // {
        //     throw new NotImplementedException();
        // }
        // public bool DeleteOneById(Guid id)
        // {
        //     var found = GetOneById(id);
        //     if (found != null)
        //     {
        //         _products.Remove(found);
        //         return true;
        //     }
        //     return false;
        // }
    }
}