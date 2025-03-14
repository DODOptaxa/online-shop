using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Store.Data.EF
{
    public class StoreDbContextFactory 
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public StoreDbContextFactory(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public StoreDbContext Create(Type repositoryType)
        {
            var services = httpContextAccessor.HttpContext.RequestServices;

            var dbContexts = services.GetService<Dictionary<Type, StoreDbContext>>();
            if (!dbContexts.ContainsKey(repositoryType))
                dbContexts[repositoryType] = services.GetService<StoreDbContext>();

            return dbContexts[repositoryType];
        }


        //---------------- DESIGN-TIME ONLY -------------------
        /*public StoreDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            var connectionString = "Data Source=.;Initial Catalog=Store;Integrated Security=true;TrustServerCertificate=true";
            optionsBuilder.UseSqlServer(connectionString);

            return new StoreDbContext(optionsBuilder.Options);
        }*/
    }
}
